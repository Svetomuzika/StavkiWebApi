using Microsoft.AspNetCore.Mvc;
using Stavki.Data.Data;
using Stavki.Infrastructure.Services.Interfaces;
using System.Security.Claims;

namespace StavkiWebApi.Controllers
{
    [Route("api/alert")]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet("getAlerts")]
        public IActionResult GetAlerts(int UserId)
        {
            return Ok(_alertService.GetAlerts(UserId));
        }

        [HttpGet("remove")]
        public IActionResult RemoveAlerts()
        {
            _alertService.RemoveAlerts();
            return Ok();
        }
    }
}
