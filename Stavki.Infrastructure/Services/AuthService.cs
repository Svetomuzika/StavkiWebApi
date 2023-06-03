using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Stavki.Data.Data;
using Stavki.Infrastructure.Auth;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Stavki.Infrastructure.Consts.ApiFns;

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

            if(string.IsNullOrEmpty(user.INN))
                throw new Exception("Не указан ИНН юр. Лица");

            

            try
            {
                using var httpClient = new HttpClient();
                    using var response = httpClient.GetAsync(string.Format(API_FNS_URL, user.INN, "c96daa40a0717105e4fcde3581abd5823d5762b1"));

                var apiResponse = response.Result.Content.ReadAsStringAsync().Result;
                var entity = (JObject)JObject.Parse(apiResponse)["items"].First["ЮЛ"];

                if (string.IsNullOrEmpty(apiResponse))
                    throw new Exception("Указан неверный ИНН Юр. лица");

                user.KPP = (string)entity["КПП"];
                user.OGRN = (string)entity["ОГРН"];
                user.CompanyName = (string)entity["НаимСокрЮЛ"];
            }
            catch (Exception e)
            {
                throw new Exception("Указан неверный ИНН юр. Лица");
            }

            var userDomain = user.MapToUserDomain();
            _userRepository.Create(userDomain);

            user.Id = userDomain.Id;
            return user;
        }

        public object ResetToken(ShortUserInfo shortUserInfo)
        {
            var identity = GetIdentity(shortUserInfo);

            // создание JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new
            {
                access_token = encodedJwt
            };
        }

        private ClaimsIdentity GetIdentity(ShortUserInfo shortUserInfo)
        {
            var user = _userRepository.Get(data => data.Email == shortUserInfo.Email).SingleOrDefault();

            if (user == null)
                throw new Exception("Почта не зарегестрирована");

            user = _userRepository.GetWithInclude(x => user.Id == x.Id
                && x.UserData.Pass == shortUserInfo.Pass, p => p.UserData).SingleOrDefault();

            if (user == null)
                throw new Exception("Неверный пароль");

            var claims = new List<Claim>
            {
                new Claim("Login", user.Email),
                new Claim("Role", user.DataSourceType.ToString())
            };

            return new ClaimsIdentity(claims, "Token");
        }

        public List<object> GetAllResponsibles()
        {
            return _userRepository.GetNotDeleted().Where(x => x.DataSourceType == Data.Data.Enums.DataSourceType.Employee).MapToShortUserInfo();
        }
    }
}
