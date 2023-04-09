using Microsoft.AspNetCore.Mvc;
using Stavki.Data.Enums;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.Services.Interfaces;

namespace StavkiWebApi.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("create")]
        public void CreateRequest(RequestDomain request) => _requestService.CreateRequest(request);

        [HttpPut("changeStatus")]
        public RequestDomain ChangeStatus(int requestId, RequestStatus status) => _requestService.ChangeStatus(requestId, status);

        [HttpGet("getRequests")]
        public List<RequestDomain> GetAllRequests() => _requestService.GetRequests();

        [HttpGet("getRequestsByUserId")]
        public List<RequestDomain> GetAllRequestsByClientId(int userId) => _requestService.GetRequestsByUserId(userId);

        [HttpGet("getRequestSum")]
        public float GetRequestSum(int weight, string city)
        {
//var gorod = unitOfWork.Gorod.GetAll();
            //var bliz = unitOfWork.BlizMezhGorodSNDS.GetAll();
            //var mezh = unitOfWork.MezhgorodSNDS.GetAll();
            //float? result = 0;

            //if (bliz.Select(x => x.City).Contains(city))
            //{
            //    if (weight < 24)
            //        result = bliz.Where(x => x.City == city).Single().Ft20;

            //    if (weight >= 24 && weight <= 27)
            //        result = bliz.Where(x => x.City == city).Single().Ft40;

            //    if (weight > 27)
            //        result = bliz.Where(x => x.City == city).Single().Ot24Do30Tn;
            //}

            //if (mezh.Select(x => x.City).Contains(city))
            //{
            //    if (weight < 24)
            //        result = mezh.Where(x => x.City == city).Single().Do24;

            //    if (weight >= 24 && weight <= 27)
            //        result = mezh.Where(x => x.City == city).Single().Ot24Do27;

            //    if (weight > 27)
            //        result = mezh.Where(x => x.City == city).Single().Ot27;
            //}

            //return result ??= 0;
            return 0;
        }

        [HttpPost("AddComment")]
        public void AddComment(int userId) => _requestService.GetRequestsByUserId(userId);

        [HttpGet("AllCities")]
        public void GetAllPuncts()
        {
            //var allPuncts = new List<string>();

            //var allBliz = unitOfWork.BlizMezhGorodSNDS.GetAll().Select(x => x.City);
            //var allMezh = unitOfWork.MezhgorodSNDS.GetAll().Select(x => x.City);

            //allPuncts.AddRange(allBliz);
            //allPuncts.AddRange(allMezh);

            //return allPuncts;
        }
    }
}
