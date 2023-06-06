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
        private readonly IAuthService _authService;

        public RequestController(IRequestService requestService, IAuthService authService)
        {
            _requestService = requestService;
            _authService = authService;
        }

        [HttpGet("getRequestsByResponsibleUserId")]
        public List<RequestDomain> GetRequestsByResponsibleUserId(int userId) => _requestService.GetRequestsByResponsibleUserId(userId);

        [HttpPost("create")]
        public int CreateRequest(RequestDomain request) => _requestService.CreateRequest(request);

        [HttpPut("changeStatus")]
        public RequestDomain ChangeStatus(int requestId, RequestStatus status) => _requestService.ChangeStatus(requestId, status);

        [HttpPost("getRequests")]
        public List<RequestDomain> GetAllRequests(SearchSettings searchSettings) => _requestService.GetRequests(searchSettings);

        //[HttpGet("getRequestsByUserId")]
        //public List<RequestDomain> GetAllRequestsByClientId(SearchSettings searchSettings) => _requestService.GetRequestsByUserId(searchSettings);

        [HttpGet("getRequestById")]
        public RequestDomain GetRequestById(int id) => _requestService.GetRequestById(id);

        [HttpGet("getRequestSum")]
        public int GetRequestSum(int weight, string city, CityType type) => _requestService.GetRequestSum(weight, city, type) ?? 0;

        [HttpGet("getCompetitors")]
        public string GetCompetitors(string city) => _requestService.GetCompetitors(city);

        [HttpPost("addComment")]
        public CommentInfo AddComment(CommentInfo comment) => _requestService.AddComment(comment);

        [HttpPut("UpdateComment")]
        public bool EditComment(CommentInfo comment) => _requestService.UpdateComment(comment);

        [HttpDelete("deleteComment")]
        public CommentInfo DeleteComment(CommentInfo comment) => _requestService.AddComment(comment);

        [HttpGet("allCities")]
        public List<ShortCityInfo> GetAllPuncts() => _requestService.GetAllCities();

        [HttpGet("getAllResponsibles")]
        public List<object> GetAllResponsibles() => _authService.GetAllResponsibles();
    }
}
