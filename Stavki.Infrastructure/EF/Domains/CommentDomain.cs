﻿using Stavki.Infrastructure.EF.Domains.Base;
using Stavki.Infrastructure.Enums;

namespace Stavki.Infrastructure.EF.Domains
{
    public class CommentDomain : BaseDomain
    {

        public int RequestId { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public string Text { get; set; }

        public DataSourceType DataSourceType { get; set; }

        public virtual RequestDomain Request { get; set; }
    }
}
