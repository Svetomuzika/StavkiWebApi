using Microsoft.AspNetCore.Mvc;
using Stavki.Data.Data;
using Stavki.Data.Data.Enums;
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

        public RequestDomain GetRequestById(int id) => _requestService.GetRequestById(id);

        [HttpGet("getRequestSum")]
        public int GetRequestSum(int weight, string city, CityType type) => _requestService.GetRequestSum(weight, city, type) ?? 0;

        [HttpPost("AddComment")]
        public RequestDomain AddComment(CommentInfo comment) => _requestService.AddComment(comment);

        [HttpGet("AllCities")]
        public List<ShortCityInfo> GetAllPuncts() => _requestService.GetAllCities();
    }
}
