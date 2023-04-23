using Stavki.Data.Data;
using Stavki.Data.Data.Enums;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure
{
    public static class Extensions
    {
        public static UserInfo MapToUserInfo(this UserDomain user)
        {
            return new UserInfo
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                DataSourceType = user.DataSourceType,
                PhoneNumber = user.PhoneNumber,
                INN = user.INN,
                CompanyName = user.CompanyName,
                OGRN = user.OGRN,
                OKPO = user.OKPO,
                KPP = user.KPP,
                Id = user.Id
            };
        }

        public static UserDomain MapToUserDomain(this UserInfo user)
        {
            return new UserDomain
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                DataSourceType = DataSourceType.Client,
                PhoneNumber = user.PhoneNumber,
                INN = user.INN,
                CompanyName = user.CompanyName,
                OGRN = user.OGRN,
                OKPO = user.OKPO,
                KPP = user.KPP,
                UserData = new UserDataDomain
                {
                    Pass = user.Pass
                }
            };
        }
    }
}
