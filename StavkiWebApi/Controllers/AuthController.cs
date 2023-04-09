using Microsoft.AspNetCore.Mvc;
using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.Services.Interfaces;

namespace StavkiWebApi.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("singIn")]
        public IActionResult SignIn(ShortUserInfo userInfo)
        {
            try
            {
                return Ok(_authService.SignIn(userInfo));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("singUn")]
        public IActionResult SingUp(UserInfo user)
        {
            try
            {
                return Ok(_authService.SignUp(user));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}