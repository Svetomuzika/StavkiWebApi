﻿using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        UserDomain SignIn(UserDataDomain userData);
        UserDomain SignUp(UserDataDomain userData);
    }
}
