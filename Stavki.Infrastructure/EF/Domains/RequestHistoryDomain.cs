using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stavki.Data.Data.Enums;
using Stavki.Infrastructure.EF.Domains.Base;

namespace Stavki.Infrastructure.EF.Domains
{
    public class RequestHistoryDomain : BaseDomain
    {
        public RequestStatus Status { get; set; }

        public string DepartureCity { get; set; }

        public string ArrivalCity { get; set; }


        public double Price { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime RequestCreateDate { get; set; }

        public string? ResponsibleUser { get; set; }

        public int? ResponsibleUserId { get; set; }

        public int RequestId { get; set; }
        public virtual RequestDomain Request { get; set; }
    }
}
