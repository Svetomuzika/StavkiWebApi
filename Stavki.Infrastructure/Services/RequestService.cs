using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stavki.Data.Data;
using Stavki.Data.Data.Enums;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.Domains.Stavki;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;
using Telegram.Bot;
using static Stavki.Infrastructure.Consts.ApiPec;


namespace Stavki.Infrastructure.Services
{
    internal class RequestService : IRequestService
    {
        private readonly IRepository<RequestDomain> _requestRepository;
        private readonly IRepository<InCityDomain> _inCityRepository;
        private readonly IRepository<NearInCityDomain> _nearInCityRepository;
        private readonly IRepository<InCityNDSDomain> _inCityNDSRepository;
        private readonly IRepository<NearInCityNDSDomain> _nearInCityNDSRepository;
        private readonly IRepository<UserDomain> _userRepository;
        private readonly IRepository<CommentDomain> _commentRepository;
        private readonly IRepository<NotifyDomain> _notifyRepository;
        private readonly IAlertService _alertService;
        private string? JobId;

        public RequestService(IRepository<RequestDomain> requestRepository, 
            IRepository<InCityDomain> inCityRepository,
            IRepository<NearInCityDomain> nearInCityRepository,
            IRepository<InCityNDSDomain> inCityNDSRepository,
            IRepository<NearInCityNDSDomain> nearInCityNDSRepository,
            IRepository<UserDomain> userRepository,
            IRepository<CommentDomain> commentRepository,
            IRepository<NotifyDomain> notifyRepository,
            IAlertService alertService)
        {
            _requestRepository = requestRepository;
            _inCityRepository = inCityRepository;
            _nearInCityRepository = nearInCityRepository;
            _inCityNDSRepository = inCityNDSRepository;
            _nearInCityNDSRepository = nearInCityNDSRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _notifyRepository = notifyRepository;
            _alertService = alertService;   
        }

        public int CreateRequest(RequestDomain req)
        {
            var responsibleUsersIdCount = _userRepository.Get(x => x.DataSourceType == 0).Count;

            var rnd = new Random();


            req.ResponsibleUserId = 1;
            //req.ResponsibleUserId = rnd.Next(1, responsibleUsersIdCount);

            var responsibleUser = _userRepository.Get(x => x.Id == req.ResponsibleUserId).FirstOrDefault();


            req.ResponsibleUser = "Алексей Белоусов";
            //req.ResponsibleUser = responsibleUser.Name + ' ' + responsibleUser.Surname;

            _requestRepository.Create(req);

            _notifyRepository.Create(new NotifyDomain()
            {
                RequestId = req.Id,
                CreatedDateTime = DateTime.Now,
                Text = "Создана новая заявка от клиента",
                UserId = req.ResponsibleUserId        
            });

            var bot = new TelegramBotClient("6100858111:AAFvM_Cp6o8NTzt0HIrrdMCkdKtwSx3wtgA");

            var aa = bot.SendTextMessageAsync("542880503", $"Создана новая заявка от клиента \nСсылка для перехода к заявке: http://localhost:5173/requests/{req.Id}").Result;
            _alertService.SendEmailAlert("", "Уведомление по заявке", $"Создана новая заявка от клиента <br/>Ссылка для перехода к заявке: http://localhost:5173/requests/{req.Id}");


            return req.Id;
        }

        public List<RequestDomain> GetRequestsByResponsibleUserId(int userId) => _requestRepository.GetWithInclude( p => p.ResponsibleUserId == userId, x => x.User, c => c.Comments).ToList();

        public List<RequestDomain> GetRequests(SearchSettings settings)
        {
            var reqs = _requestRepository.GetWithInclude(x => x.User, c => c.Comments);


            if (settings.ClientId is not null)
                reqs = reqs.Where(x => x.UserId == settings.ClientId);


            if (settings.Cities.Any())
                reqs = reqs.Where(x => settings.Cities.Contains(x.DepartureCity));

            if (settings.RequestStatuses.Any())
                reqs = reqs.Where(x => settings.RequestStatuses.Contains(x.Status));

            if (settings.Responsibles.Any())
                reqs = reqs.Where(x => settings.Responsibles.Contains(x.ResponsibleUserId));
            \
            if(settings.StartDate is not null)
                reqs = reqs.Where(x => x.DepartureDate > settings.StartDate);

            if (settings.EndDate is not null)
                reqs = reqs.Where(x => x.DepartureDate < settings.EndDate);

            reqs = reqs.Where(x => settings.StartPrice < x.Price && settings.EndPrice > x.Price)
                .Where(x => settings.StartWeight < x.CargoWeight && settings.EndWeight > x.CargoWeight);

            return reqs.ToList();
        } 

        public List<RequestDomain> GetRequestsByUserId(int userId) => _requestRepository.GetWithInclude(req => req.UserId == userId, c => c.Comments).ToList();

        public RequestDomain GetRequestById(int id) => _requestRepository.GetWithInclude(x => x.Id == id, c => c.User, e => e.Comments).FirstOrDefault();

        public RequestDomain ChangeStatus(int id, RequestStatus status)
        {
            var request = _requestRepository.FindById(id);

            request.Status = status;

            _requestRepository.Update(request);

            return request;
        }

        public List<ShortCityInfo> GetAllCities()
        {
            var allPuncts = new List<ShortCityInfo>();

            var allNearInCity = _nearInCityRepository.GetNotDeleted().Select(x => new ShortCityInfo
            {
                City = x.City,
                Type = CityType.NearInCity
            });

            var allInCity = _inCityRepository.GetNotDeleted().Select(x => new ShortCityInfo
            {
                City = x.City,
                Type = CityType.InCity
            });

            allPuncts.AddRange(allNearInCity);
            allPuncts.AddRange(allInCity);

            return allPuncts;
        }

        public int? GetRequestSum(int weight, string city, CityType type)
        {
            switch (type)
            {
                case CityType.InCityNDS:
                    switch (weight)
                    {
                        case < 24:
                            return _inCityNDSRepository.Get(x => x.City.Contains(city)).First().UpTo24Tons;
                        case > 25 and < 27:
                            return _inCityNDSRepository.Get(x => x.City.Contains(city)).First().From24UpTo27Tons;
                        case > 27:
                            return _inCityNDSRepository.Get(x => x.City.Contains(city)).First().From27Tons;
                    }
                    break;

                case CityType.InCity:
                    switch (weight)
                    {
                        case < 24:
                            return _inCityRepository.Get(x => x.City.Contains(city)).First().UpTo24Tons;
                        case > 25 and < 27:
                            return _inCityRepository.Get(x => x.City.Contains(city)).First().From24UpTo27Tons;
                        case > 27:
                            return _inCityRepository.Get(x => x.City.Contains(city)).First().From27Tons;
                    }
                    break;

                case CityType.NearInCityNDS:
                    switch (weight)
                    {
                        case < 24:
                            return _nearInCityNDSRepository.Get(x => x.City.Contains(city)).First().Feet20;
                        case > 25 and < 27:
                            return _nearInCityNDSRepository.Get(x => x.City.Contains(city)).First().Feet40;
                        case > 27:
                            return _nearInCityNDSRepository.Get(x => x.City.Contains(city)).First().From24UpTo30Tons;
                    }
                    break;

                case CityType.NearInCity:
                    switch (weight)
                    {
                        case < 24:
                            return _nearInCityRepository.Get(x => x.City.Contains(city)).First().Feet20;
                        case > 25 and < 27:
                            return _nearInCityRepository.Get(x => x.City.Contains(city)).First().Feet40;
                        case > 27:
                            return _nearInCityRepository.Get(x => x.City.Contains(city)).First().From24UpTo30Tons;
                    }
                    break;
            }

            return 0;
        }

        public RequestDomain AddComment(CommentInfo comment)
        {
            var user = _userRepository.Get(x => x.Id == comment.UserId).First();

            _commentRepository.Create(new CommentDomain
            {
                RequestId = comment.RequestId,
                UserId = comment.UserId,
                DataSourceType = user.DataSourceType,
                CreateDate = DateTime.Now,
                Text = comment.Comment,
            });

            JobId = BackgroundJob.Schedule(() => SendDelayedMessagesJob(comment.RequestId), TimeSpan.FromSeconds(10));

            return _requestRepository.GetWithInclude(x => x.Comments).First(x => x.Id == comment.RequestId);
        }

        public bool UpdateComment(CommentInfo comment)
        {
            var user = _userRepository.Get(x => x.Id == comment.UserId).First();

            var commentEntity = _commentRepository.FindById(comment.Id);

            commentEntity.Text = comment.Comment;

            _commentRepository.Update(commentEntity);

            BackgroundJob.Delete(JobId);

            JobId = BackgroundJob.Schedule(() => SendDelayedMessagesJob(comment.RequestId), TimeSpan.FromSeconds(10));

            return true;
        }

        public string? GetCompetitors(string city)
        {
            var width = 1;
            var length = 1;
            var height = 1;
            var volume = 1;
            var weight = 1;

            using var httpClient = new HttpClient();
                using var citiesResponse = httpClient.GetAsync(API_PEC_CITIES_URL);

            var citiesApiResponse = citiesResponse.Result.Content.ReadAsStringAsync().Result;

            Regex regex = new Regex(@"\\U([0-9A-F]{4})", RegexOptions.IgnoreCase);

            var result = regex
                .Replace(citiesApiResponse, match => ((char)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber))
                .ToString());

            var cityId = result
                .Split(new char[] { ',' })
                .Select(x => x.Split(new char[] { ':' }))
                .Where(x => x.Count() > 1)
                .FirstOrDefault(x => x[1].ToLower().Contains(city.ToLower()))?.FirstOrDefault();

            using var priceResponse = httpClient.GetAsync(string.Format(API_PEC_PRICE_URL, width, length, height, volume, weight, cityId));

            var priceApiResponse = priceResponse.Result.Content.ReadAsStringAsync().Result;

            result = regex
                .Replace(priceApiResponse, match => ((char)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber))
                .ToString());

            return (string)JObject.Parse(result)["deliver"][2];
        }

        public void SendDelayedMessagesJob(int id)
        {
            var req = _requestRepository.FindById(id);

            _notifyRepository.Create(new NotifyDomain
            {
                RequestId = id,
                Text = "В заявке оставлен новый комментарий",
                CreatedDateTime = DateTime.Now,
                UserId = req.UserId
            });

            var bot = new TelegramBotClient("6100858111:AAFvM_Cp6o8NTzt0HIrrdMCkdKtwSx3wtgA");

            var aa = bot.SendTextMessageAsync("542880503", $"В заявке №{id} оставлен новый комментарий \nСсылка для перехода к заявке: http://localhost:5173/requests/{id}").Result;

            _alertService.SendEmailAlert("", "Уведомление по заявке", $"В заявке №{id} оставлен новый комментарий <br/>Ссылка для перехода к заявке: http://localhost:5173/requests/{id}");
        }
    }
}
