using Newtonsoft.Json.Linq;
using Stavki.Data.Data;
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

            return user.MapToUserInfo();
        } 

        public UserInfo SignUp(UserInfo user)
        {
            if (_userRepository.Get(data => data.Email == user.Email).Any())
                throw new Exception("Почта уже зарегестрирована");

            using (var httpClient = new HttpClient())
            {
                using var response = 
                    httpClient.GetAsync($"https://api-fns.ru/api/egr?req={user.INN}&key=c96daa40a0717105e4fcde3581abd5823d5762b1");

                var apiResponse = response.Result.Content.ReadAsStringAsync().Result;
                var entity = (JObject)JObject.Parse(apiResponse)["items"].First["ЮЛ"];

                user.KPP = (string)entity["КПП"];
                user.OGRN = (string)entity["ОГРН"];
                user.CompanyName = (string)entity["НаимСокрЮЛ"];
            }

            var userDomain = user.MapToUserDomain();
            _userRepository.Create(userDomain);

            user.Id = userDomain.Id;
            return user;
        }
    }
}
