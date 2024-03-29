﻿using Stavki.Data.Data.Enums;
using Stavki.Infrastructure.EF.Domains.Base;

namespace Stavki.Infrastructure.EF.Domains
{
    public class UserDomain : BaseDomain
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string INN { get; set; }

        public string? KPP { get; set; }

        public string? OGRN { get; set; }

        public string? OKPO { get; set; }

        public DataSourceType DataSourceType { get; set; }

        public UserDataDomain UserData{ get; set; }

        public List<RequestDomain> Requests { get; set; }

        public List<RequestHistoryDomain> RequestsHistory { get; set; }

    }
}
