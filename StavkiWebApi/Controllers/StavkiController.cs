﻿using Microsoft.AspNetCore.Mvc;
using Stavki.Infrastructure.EF.Domains.Stavki;
using Stavki.Infrastructure.Services.Interfaces;


namespace StavkiWebApi.Controllers
{
    [ApiController]
    [Route("api/stavki")]
    public class StavkiController : Controller
    {
        private readonly ICalcService _calcService;

        public StavkiController(ICalcService calcService)
        {
            _calcService = calcService;
        }

        [HttpGet("inCity")]
        public List<InCityDomain> GetStavkiInCity()
        {
            return _calcService.GetStavkiInCity();
        }

        [HttpGet("inCityNDS")]
        public List<InCityNDSDomain> GetStavkiInCityNDS()
        {
            return _calcService.GetStavkiInCityNDS();
        }

        [HttpGet("nearInCity")]
        public List<NearInCityDomain> GetStavkiNearInCity()
        {
            return _calcService.GetStavkiNearInCity();
        }

        [HttpGet("nearInCityNDS")]
        public List<NearInCityNDSDomain> GetStavkiNearInCityNDS()
        {
            return _calcService.GetStavkiNearInCityNDS();
        }
    }
}
