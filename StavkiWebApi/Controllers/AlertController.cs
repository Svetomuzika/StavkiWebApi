using Microsoft.AspNetCore.Mvc;
using Stavki.Data.Data;
using Stavki.Infrastructure.Services.Interfaces;
using System.Security.Claims;

namespace StavkiWebApi.Controllers
{
    [Route("api/alert")]
    public class AlertController : ControllerBase
    {
        [HttpPost("getAlertsCount")]
        public IActionResult SignIn()
        {
            return Ok();
        }
    }
}
