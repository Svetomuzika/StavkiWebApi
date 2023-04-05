using Microsoft.AspNetCore.Mvc;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.Enums;
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

        [HttpPost("getRequestsByUserId")]
        public List<RequestDomain> GetAllRequestsByClientId(int userId) => _requestService.GetRequestsByUserId(userId);

    }
}
