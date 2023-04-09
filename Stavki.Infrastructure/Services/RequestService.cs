using Stavki.Data.Data;
using Stavki.Data.Enums;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;

namespace Stavki.Infrastructure.Services
{
    internal class RequestService : IRequestService
    {
        private readonly IRepository<RequestDomain> _requestRepository;

        public RequestService(IRepository<RequestDomain> requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public void CreateRequest(RequestDomain req) => _requestRepository.Create(req);

        public List<RequestDomain> GetRequests() => _requestRepository.Get();

        public List<RequestDomain> GetRequestsByUserId(int userId) => _requestRepository.Get(req => req.UserId == userId);

        public RequestDomain ChangeStatus(int id, RequestStatus status)
        {
            var request = _requestRepository.FindById(id);

            request.Status = status;

            _requestRepository.Update(request);

            return request;
        }
    }
}
