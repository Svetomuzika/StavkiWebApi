using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stavki.Data.Data;
using Stavki.Data.Data.Enums;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.Domains.Stavki;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;
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

        public RequestService(IRepository<RequestDomain> requestRepository, 
            IRepository<InCityDomain> inCityRepository,
            IRepository<NearInCityDomain> nearInCityRepository,
            IRepository<InCityNDSDomain> inCityNDSRepository,
            IRepository<NearInCityNDSDomain> nearInCityNDSRepository,
            IRepository<UserDomain> userRepository,
            IRepository<CommentDomain> commentRepository)
        {
            _requestRepository = requestRepository;
            _inCityRepository = inCityRepository;
            _nearInCityRepository = nearInCityRepository;
            _inCityNDSRepository = inCityNDSRepository;
            _nearInCityNDSRepository = nearInCityNDSRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public int CreateRequest(RequestDomain req)
        {
            var responsibleUsersIdCount = _userRepository.Get(x => x.DataSourceType == 0).Count;

            var rnd = new Random();

            req.ResponsibleUserId = rnd.Next(1, responsibleUsersIdCount);

            var responsibleUser = _userRepository.Get(x => x.Id == req.ResponsibleUserId).FirstOrDefault();

            req.ResponsibleUser = responsibleUser.Name + ' ' + responsibleUser.Surname;

            _requestRepository.Create(req);

            return req.Id;
        }

        public List<RequestDomain> GetRequestsByResponsibleUserId(int userId) => _requestRepository.GetWithInclude( p => p.ResponsibleUserId == userId, x => x.User, c => c.Comments).ToList();

        public List<RequestDomain> GetRequests() => _requestRepository.GetWithInclude(x => x.User, c => c.Comments).ToList();

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

            return _requestRepository.GetWithInclude(x => x.Comments).First(x => x.Id == comment.RequestId);
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
    }
}
