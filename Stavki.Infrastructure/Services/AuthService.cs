﻿using Stavki.Data.Data;
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

            var userDomain = user.MapToUserDomain();

            _userRepository.Create(userDomain);

            user.Id = userDomain.Id;
            return user;
        }
    }
}