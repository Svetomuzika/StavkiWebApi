using Stavki.Data.Data;
using Stavki.Data.Data.Enums;
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

        public bool UpdateStavka(GeneralStavka generalStavka)
        {
            try
            {
                switch (generalStavka.CityType)
                {
                    case CityType.InCity:
                    {
                        var stavka = _inCityRepository.FindById(generalStavka.Id);

                        stavka.UpTo24Tons = generalStavka.FirstValue;
                        stavka.From24UpTo27Tons = generalStavka.SecondValue;
                        stavka.From27Tons = generalStavka.ThirdValue;
                        stavka.Distance = generalStavka.Distance;
                        stavka.City = generalStavka.City;

                        _inCityRepository.Update(stavka);
                    }
                    break;

                    case CityType.InCityNDS:
                    {
                        var stavka = _inCityNDSRepository.FindById(generalStavka.Id);

                        stavka.UpTo24Tons = generalStavka.FirstValue;
                        stavka.From24UpTo27Tons = generalStavka.SecondValue;
                        stavka.From27Tons = generalStavka.ThirdValue;
                        stavka.Distance = generalStavka.Distance;
                        stavka.City = generalStavka.City;

                        _inCityNDSRepository.Update(stavka);
                    }
                    break;

                    case CityType.NearInCity:
                    {
                        var stavka = _nearInCityRepository.FindById(generalStavka.Id);

                        stavka.Feet20 = generalStavka.FirstValue;
                        stavka.From24UpTo30Tons = generalStavka.SecondValue;
                        stavka.Feet40 = generalStavka.ThirdValue;
                        stavka.Distance = generalStavka.Distance;
                        stavka.City = generalStavka.City;

                        _nearInCityRepository.Update(stavka);
                    }
                    break;

                    case CityType.NearInCityNDS:
                    {
                        var stavka = _nearInCityNDSRepository.FindById(generalStavka.Id);

                        stavka.Feet20 = generalStavka.FirstValue;
                        stavka.From24UpTo30Tons = generalStavka.SecondValue;
                        stavka.Feet40 = generalStavka.ThirdValue;
                        stavka.Distance = generalStavka.Distance;
                        stavka.City = generalStavka.City;

                        _nearInCityNDSRepository.Update(stavka);
                    }
                    break;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
