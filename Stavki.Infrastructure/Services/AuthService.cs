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

        public UserDomain SignIn(UserInfo userInfo) => _userRepository.Get(data => data.Email == userInfo.Email)
            .SingleOrDefault(user => user.UserData.Pass == userInfo.Pass);

        public void SignUp(UserDomain user)
        {
            if (_userRepository.Get(data => data.Email == user.Email).Any())
                throw new Exception("Почта уже зарегестрирована");

            _userRepository.Create(user);
        }
    }
}
