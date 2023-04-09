using Stavki.Infrastructure.EF.Domains.Base;

namespace Stavki.Infrastructure.EF.Domains
{
    public class UserDataDomain : BaseDomain
    {
        public int UserId { get; set; }

        public string Pass { get; set; }

        public UserDomain User { get; set; }
    }
}
