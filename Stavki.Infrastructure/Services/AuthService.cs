using Stavki.Data.Data;
using Stavki.Data.Enums;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;

namespace Stavki.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserDomain> _userRepository;

        public AuthService(IRepository<UserDomain> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserInfo SignIn(ShortUserInfo shortUserInfo)
        {
            var user = _userRepository.Get(data => data.Email == shortUserInfo.Email).SingleOrDefault();

            if(user == null)
                throw new Exception("Почта не зарегестрирована");

            user = _userRepository.GetWithInclude(x => user.Id == x.Id 
                && x.UserData.Pass == shortUserInfo.Pass, p => p.UserData).SingleOrDefault();

            if (user == null)
                throw new Exception("Неверный пароль");

            var userInfo = new UserInfo()
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

            return userInfo;
        } 

        public UserInfo SignUp(UserInfo user)
        {
            if (_userRepository.Get(data => data.Email == user.Email).Any())
                throw new Exception("Почта уже зарегестрирована");

            var userDomain = new UserDomain()
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

            _userRepository.Create(userDomain);

            user.Id = userDomain.Id;
            return user;
        }
    }
}
