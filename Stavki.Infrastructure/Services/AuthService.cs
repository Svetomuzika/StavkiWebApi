using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;

namespace Stavki.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserDomain> _userRepository;
        private readonly IRepository<UserDataDomain> _userDataRepository;

        public AuthService(IRepository<UserDomain> userRepository, IRepository<UserDataDomain> userDataRepository)
        {
            _userRepository = userRepository;
            _userDataRepository = userDataRepository;
        }

        public UserDomain SignIn(UserDataDomain userData)
        {
            if(_userDataRepository.Get(data => data.Email == userData.Email).SingleOrDefault()?.Pass == userData.Pass)
                return _userRepository.Get(user => user.Email == userData.Email).SingleOrDefault();

            return null;
            //string someString = Encoding.ASCII.GetString(bytes);
        }

        public UserDomain SignUp(UserDataDomain userData)
        {
            if (_userDataRepository.Get(data => data.Email == userData.Email).SingleOrDefault()?.Pass == userData.Pass)
                return _userRepository.Get(user => user.Email == userData.Email).SingleOrDefault();

            return null;
            //string someString = Encoding.ASCII.GetString(bytes);
        }
    }
}
