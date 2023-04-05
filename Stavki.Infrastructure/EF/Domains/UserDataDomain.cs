using Stavki.Infrastructure.EF.Domains.Base;

namespace Stavki.Infrastructure.EF.Domains
{
    public class UserDataDomain : BaseDomain
    {
        public string Email { get; set; }

        public string Pass { get; set; }
    }
}
