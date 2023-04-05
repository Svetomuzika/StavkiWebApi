using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stavki.Data.Data;
using Stavki.Infrastructure;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Enums;
using Stavki.Infrastructure.Services;
using Stavki.Infrastructure.Services.Interfaces;

namespace StavkiWebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("signIn")]
        public UserDomain SignIn(UserDataDomain userData) => _authService.SignIn(userData);

        [HttpPost("singUp")]
        public UserDomain SingUp(UserDataDomain userData) => _authService.SignUp(userData);
    }
}