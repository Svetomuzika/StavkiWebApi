using Stavki.Data.Data.Enums;

namespace Stavki.Data.Data
{
    public class SearchSettings
    {
        public List<RequestStatus> RequestStatuses { get; set; }

        public List<int?> Responsibles { get; set; }

        public int StartWeight { get; set; }

        public int EndWeight { get; set; }

        public int StartPrice { get; set; }

        public int EndPrice { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ClientId { get; set; }
    }
}
