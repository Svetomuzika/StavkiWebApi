using Stavki.Data.Data;
using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.Enums;

namespace Stavki.Infrastructure.Services.Interfaces
{
    public interface IRequestService
    {
        void CreateRequest(RequestDomain req);

        RequestDomain ChangeStatus(int id, RequestStatus status);

        List<RequestDomain> GetRequests();

        List<RequestDomain> GetRequestsByUserId(int userId);
    }
}
