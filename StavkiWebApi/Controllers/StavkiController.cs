using Microsoft.AspNetCore.Mvc;
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

        //[HttpGet("Stavki/inCity")]
        //public void GetStavkiGorod()
        //{
        //    //var allCities = unitOfWork.Gorod.GetAll().ToList();

        //    //foreach (var city in allCities)
        //    //{
        //    //    city.Ft20 = (int)(city.Ft20 * 0.8);
        //    //    city.Ft40 = (int)(city.Ft40 * 0.8);
        //    //    city.Ot24Do30Tn = (int)(city.Ot24Do30Tn * 0.8);
        //    //}

        //    //return allCities;
        //}

        [HttpGet("nearInCityNDS")]
        public void GetStavkiBlizMezhGorodSNDS()
        {
            //
        }

        [HttpGet("nearInCity")]
        public void GetStavkiBlizMezhGorod()
        {
            //var allBliz = unitOfWork.BlizMezhGorodSNDS.GetAll().ToList();

            //foreach (var city in allBliz)
            //{
            //    city.Ft20 = (int)(city.Ft20 * 0.8);
            //    city.Ft40 = (int)(city.Ft40 * 0.8);
            //    city.Ot24Do30Tn = (int)(city.Ot24Do30Tn * 0.8);
            //}

            //return allBliz;
        }

        [HttpGet("inCityNDS")]
        public void GetStavkiMezhgorodSNDS()
        {
            //unitOfWork.MezhgorodSNDS.GetAll().ToList();
        }

        [HttpGet("inCity")]
        public void voidGetStavkiMezhgorod()
        {
            //var allMezh = unitOfWork.MezhgorodSNDS.GetAll().ToList();

            //foreach (var city in allMezh)
            //{
            //    city.Ot27 = city.Ot27 is null ? 0 : (int)(city.Ot27 * 0.8);
            //    city.Do24 = city.Do24 is null ? 0 : (int)(city.Ot27 * 0.8);
            //    city.Ot24Do27 = city.Ot24Do27 is null ? 0 : (int)(city.Ot27 * 0.8);
            //}

            //return allMezh;
        }
    }
}
