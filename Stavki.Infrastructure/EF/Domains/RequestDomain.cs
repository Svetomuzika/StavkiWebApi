using Stavki.Data.Enums;
using Stavki.Infrastructure.EF.Domains.Base;

namespace Stavki.Infrastructure.EF.Domains
{
    public class RequestDomain : BaseDomain
    {
        public RequestStatus Status { get; set; }

        public string DepartureCity { get; set; }

        public string ArrivalCity { get; set; }

        public int ContainerSize { get; set; }

        public int CargoWeight { get; set; }

        public double Price { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime RequestCreateDate { get; set; }

        public List<CommentDomain> Comments { get; set; }

        public int UserId { get; set; }
    }
}
