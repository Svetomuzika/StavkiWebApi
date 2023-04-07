using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.Services.Interfaces;

namespace StavkiWebApi.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPost("api/signIn")]
        public UserDomain SignIn(UserInfo userInfo) => _authService.SignIn(userInfo);

        [Microsoft.AspNetCore.Mvc.HttpPost("api/singUp")]
        public IHttpActionResult SingUp(UserDomain user)
        {
            try
            {
                _authService.SignUp(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message, this);
            }
        }
    }
}