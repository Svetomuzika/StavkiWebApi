using Stavki.Data.Data;
using Stavki.Data.Data.Enums;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.Domains.Stavki;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;

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

        public void CreateRequest(RequestDomain req) => _requestRepository.Create(req);

        public List<RequestDomain> GetRequests() => _requestRepository.Get();

        public List<RequestDomain> GetRequestsByUserId(int userId) => _requestRepository.Get(req => req.UserId == userId);

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

            var allNearInCity = _nearInCityRepository.Get().Select(x => new ShortCityInfo
            {
                City = x.City,
                Type = CityType.NearInCity
            });

            var allInCity = _inCityRepository.Get().Select(x => new ShortCityInfo
            {
                City = x.City,
                Type = CityType.InCity
            });

            allPuncts.AddRange(allNearInCity);
            allPuncts.AddRange(allInCity);

            return allPuncts;
        }

        public int? GetRequestSum(int weight, string city, bool nds, CityType type)
        {
            switch (type)
            {
                case CityType.InCity:
                {
                    if (nds)
                        switch (weight)
                        {
                            case < 24:
                                return _inCityNDSRepository.Get(x => x.City.Contains(city)).First().UpTo24Tons;
                            case > 25 and < 27:
                                return _inCityNDSRepository.Get(x => x.City.Contains(city)).First().From24UpTo27Tons;
                            case > 27:
                                return _inCityNDSRepository.Get(x => x.City.Contains(city)).First().From27Tons;
                        }

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
                }
                case CityType.NearInCity:
                {
                    if (nds)
                        switch (weight)
                        {
                            case < 24:
                                return _nearInCityNDSRepository.Get(x => x.City.Contains(city)).First().Feet20;
                            case > 25 and < 27:
                                return _nearInCityNDSRepository.Get(x => x.City.Contains(city)).First().Feet40;
                            case > 27:
                                return _nearInCityNDSRepository.Get(x => x.City.Contains(city)).First().From24UpTo30Tons;
                        }

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
    }
}
