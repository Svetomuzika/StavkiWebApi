using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.Domains.Stavki;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;

namespace Stavki.Infrastructure.Services
{
    public class CalcService : ICalcService
    {
        private readonly IRepository<InCityDomain> _inCityRepository;
        private readonly IRepository<NearInCityDomain> _nearInCityRepository;
        private readonly IRepository<InCityNDSDomain> _inCityNDSRepository;
        private readonly IRepository<NearInCityNDSDomain> _nearInCityNDSRepository;

        public CalcService(IRepository<InCityDomain> inCityRepository,
            IRepository<NearInCityDomain> nearInCityRepository,
            IRepository<InCityNDSDomain> inCityNDSRepository,
            IRepository<NearInCityNDSDomain> nearInCityNDSRepository)
        {
            _inCityRepository = inCityRepository;
            _nearInCityRepository = nearInCityRepository;
            _inCityNDSRepository = inCityNDSRepository;
            _nearInCityNDSRepository = nearInCityNDSRepository;
        }

        public List<InCityDomain> GetStavkiInCity()
        {
            return _inCityRepository.Get();
        }

        public List<InCityNDSDomain> GetStavkiInCityNDS()
        {
            return _inCityNDSRepository.Get();
        }

        public List<NearInCityDomain> GetStavkiNearInCity()
        {
            return _nearInCityRepository.Get();
        }

        public List<NearInCityNDSDomain> GetStavkiNearInCityNDS()
        {
            return _nearInCityNDSRepository.Get();
        }
    }
}
