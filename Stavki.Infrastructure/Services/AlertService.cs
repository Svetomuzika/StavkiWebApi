using Stavki.Infrastructure.EF.Domains;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services.Interfaces;

namespace Stavki.Infrastructure.Services
{
    public class AlertService : IAlertService
    {
        private readonly IRepository<NotifyDomain> _notifyRepository;

        public AlertService(IRepository<NotifyDomain> notifyRepository)
        {
            _notifyRepository = notifyRepository;
        }

        public List<NotifyDomain> GetAlerts(int userId)
        {
            return _notifyRepository.Get().Where(x => x.UserId == userId && !x.IsDeleted).ToList();
        }

        public void RemoveAlerts()
        {
            var alerts = _notifyRepository.Get().Where(x => x.IsDeleted == false).ToList();

            alerts.ForEach(x =>
            {
                x.IsDeleted =  true;
                _notifyRepository.Update(x);
            });
        }
    }
}
