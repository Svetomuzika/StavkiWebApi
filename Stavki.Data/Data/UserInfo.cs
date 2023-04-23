using Stavki.Data.Data.Enums;

namespace Stavki.Data.Data
{
    public class UserInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string INN { get; set; }

        public string KPP { get; set; }

        public string OGRN { get; set; }

        public string OKPO { get; set; }

        public string Pass { get; set; }

        public int Id { get; set; }

        public DataSourceType DataSourceType { get; set; }
    }
}
