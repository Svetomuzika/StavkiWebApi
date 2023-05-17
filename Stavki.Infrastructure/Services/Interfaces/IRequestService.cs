using Stavki.Data.Data;
using Stavki.Data.Data.Enums;
using Stavki.Infrastructure.EF.Domains;

namespace Stavki.Infrastructure.Services.Interfaces
{
    public interface IRequestService
    {
        void CreateRequest(RequestDomain req);

        RequestDomain ChangeStatus(int id, RequestStatus status);

        List<RequestDomain> GetRequests();

        List<RequestDomain> GetRequestsByUserId(int userId);

        List<ShortCityInfo> GetAllCities();

        int? GetRequestSum(int weight, string city, CityType type);

        RequestDomain AddComment(CommentInfo comment);

        RequestDomain GetRequestById(int id);
    }
}
