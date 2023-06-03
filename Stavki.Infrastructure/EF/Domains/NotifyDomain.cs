using Stavki.Infrastructure.EF.Domains.Base;

namespace Stavki.Infrastructure.EF.Domains
{
    public class NotifyDomain : BaseDomain
    {
        public string Text { get; set; }

        public string Subject { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? SentDateTime { get; set; }

        public bool IsError { get; set; }

        public string Addressee { get; set; }

        public bool IsSent { get; set; }

        public int RequestId { get; set; }

        public RequestDomain Request { get; set; }
    }
}
