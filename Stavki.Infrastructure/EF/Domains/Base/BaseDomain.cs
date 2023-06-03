namespace Stavki.Infrastructure.EF.Domains.Base
{
    public abstract class BaseDomain
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
