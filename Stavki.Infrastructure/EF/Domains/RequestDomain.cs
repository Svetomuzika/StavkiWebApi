﻿using System.ComponentModel.DataAnnotations;
using Stavki.Data.Data.Enums;
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

        public List<CommentDomain> Comments { get; set; } = new List<CommentDomain>();
        public List<RequestHistoryDomain> RequestHistory { get; set; } = new List<RequestHistoryDomain>();

        public string? ResponsibleUser { get; set; }

        public int? ResponsibleUserId { get; set; }

        public int UserId { get; set; }

        public UserDomain? User { get; set; }

        public List<NotifyDomain> Notifications { get; set; } = new List<NotifyDomain>();
    }
}
