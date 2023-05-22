using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains.Stavki;

namespace Stavki.Infrastructure.Services.Interfaces
{
    public interface ICalcService
    {
        public List<NearInCityNDSDomain> GetStavkiNearInCityNDS();

        public List<NearInCityDomain> GetStavkiNearInCity();

        public List<InCityNDSDomain> GetStavkiInCityNDS();

        public List<InCityDomain> GetStavkiInCity();

        bool UpdateStavka(GeneralStavka stavka);

        public bool DeleteStavka(GeneralStavka generalStavka)

        public bool AddStavka(GeneralStavka generalStavka)
    }
}
