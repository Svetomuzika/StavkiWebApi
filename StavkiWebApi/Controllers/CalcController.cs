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
    [Route("api/calc")]
    public class CalcController : ControllerBase
    {
        private readonly ICalcService _calcService;

        public CalcController(ICalcService calcService)
        {
            _calcService = calcService;
        }

        //[HttpGet("Stavki/Gorod")]
        //public IEnumerable<Gorod> GetStavkiGorod()
        //{
        //    var allCities = unitOfWork.Gorod.GetAll().ToList();

        //    foreach (var city in allCities)
        //    {
        //        city.Ft20 = (int)(city.Ft20 * 0.8);
        //        city.Ft40 = (int)(city.Ft40 * 0.8);
        //        city.Ot24Do30Tn = (int)(city.Ot24Do30Tn * 0.8);
        //    }

        //    return allCities;
        //}

        //[HttpGet("Stavki/BlizMezhGorodSNDS")]
        //public IEnumerable<BlizMezhGorodSNDS> GetStavkiBlizMezhGorodSNDS() => unitOfWork.BlizMezhGorodSNDS.GetAll().ToList();

        //[HttpGet("Stavki/BlizMezhGorod")]
        //public IEnumerable<BlizMezhGorodSNDS> GetStavkiBlizMezhGorod()
        //{
        //    var allBliz = unitOfWork.BlizMezhGorodSNDS.GetAll().ToList();

        //    foreach (var city in allBliz)
        //    {
        //        city.Ft20 = (int)(city.Ft20 * 0.8);
        //        city.Ft40 = (int)(city.Ft40 * 0.8);
        //        city.Ot24Do30Tn = (int)(city.Ot24Do30Tn * 0.8);
        //    }

        //    return allBliz;
        //}

        //[HttpGet("Stavki/MezhgorodSNDS")]
        //public IEnumerable<MezhgorodSNDS> GetStavkiMezhgorodSNDS() => unitOfWork.MezhgorodSNDS.GetAll().ToList();

        //[HttpGet("Stavki/Mezhgorod")]
        //public IEnumerable<MezhgorodSNDS> GetStavkiMezhgorod()
        //{
        //    var allMezh = unitOfWork.MezhgorodSNDS.GetAll().ToList();

        //    foreach (var city in allMezh)
        //    {
        //        city.Ot27 = city.Ot27 is null ? 0 : (int)(city.Ot27 * 0.8);
        //        city.Do24 = city.Do24 is null ? 0 : (int)(city.Ot27 * 0.8);
        //        city.Ot24Do27 = city.Ot24Do27 is null ? 0 : (int)(city.Ot27 * 0.8);
        //    }

        //    return allMezh;
        //}

        //[HttpGet("Stavki/GetAllPuncts")]
        //public IEnumerable<string> GetAllPuncts()
        //{
        //    var allPuncts = new List<string>();

        //    var allBliz = unitOfWork.BlizMezhGorodSNDS.GetAll().Select(x => x.City);
        //    var allMezh = unitOfWork.MezhgorodSNDS.GetAll().Select(x => x.City);

        //    allPuncts.AddRange(allBliz);
        //    allPuncts.AddRange(allMezh);

        //    return allPuncts;
        //}

        //[HttpGet("Requests/GetRequestSum")]
        //public float GetRequestSum(int weight, string city)
        //{
        //    var gorod = unitOfWork.Gorod.GetAll();
        //    var bliz = unitOfWork.BlizMezhGorodSNDS.GetAll();
        //    var mezh = unitOfWork.MezhgorodSNDS.GetAll();
        //    float? result = 0;

        //    if (bliz.Select(x => x.City).Contains(city))
        //    {
        //        if (weight < 24)
        //            result = bliz.Where(x => x.City == city).Single().Ft20;

        //        if (weight >= 24 && weight <= 27)
        //            result = bliz.Where(x => x.City == city).Single().Ft40;

        //        if (weight > 27)
        //            result = bliz.Where(x => x.City == city).Single().Ot24Do30Tn;
        //    }

        //    if (mezh.Select(x => x.City).Contains(city))
        //    {
        //        if (weight < 24)
        //            result = mezh.Where(x => x.City == city).Single().Do24;

        //        if (weight >= 24 && weight <= 27)
        //            result = mezh.Where(x => x.City == city).Single().Ot24Do27;

        //        if (weight > 27)
        //            result = mezh.Where(x => x.City == city).Single().Ot27;
        //    }

        //    return result ??= 0;
        //}

    }
}
