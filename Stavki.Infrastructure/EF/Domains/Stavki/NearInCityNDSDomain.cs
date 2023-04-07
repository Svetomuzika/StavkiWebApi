using Stavki.Infrastructure.EF.Domains.Base;

namespace Stavki.Infrastructure.EF.Domains.Stavki
{
    public class NearInCityNDSDomain : BaseStavkiDomain
    {
        public int? Feet20 { get; set; }

        public int? Feet40 { get; set; }

        public int? From24UpTo30Tons { get; set; }
    }
}
