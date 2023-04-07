using Stavki.Infrastructure.EF.Domains.Base;

namespace Stavki.Infrastructure.EF.Domains.Stavki
{
    public class InCityDomain : BaseStavkiDomain
    {
        public int? UpTo24Tons { get; set; }

        public int? From24UpTo27Tons{ get; set; }

        public int? From27Tons { get; set; }
    }
}
