using Microsoft.AspNetCore.Mvc;
using Stavki.Data.Data;
using Stavki.Infrastructure.Services.Interfaces;
using System.Security.Claims;

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

        [HttpPost("signIn")]
        public IActionResult SignIn(ShortUserInfo userInfo)
        {
            try
            {
                var token = _authService.ResetToken(userInfo);
                var user = _authService.SignIn(userInfo);

                var result = new
                {
                    user,
                    token
                };

                return Ok(new{ user, token });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("singUp")]
        public IActionResult SingUp(UserInfo user)
        {
            try
            {
                var user = _authService.SignUp(user);

                var result = new
                {
                    user,
                    token = ""
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("resetToken")]
        public IActionResult Token(ShortUserInfo userInfo)
        {
            return Ok(_authService.ResetToken(userInfo));
        }
    }
}