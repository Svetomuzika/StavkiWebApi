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

        public NotifyDomain GetAlerts(int userId)
        {
            return _notifyRepository.Get().FirstOrDefault(x => x.UserId == userId);
        }

        public void RemoveAlerts()
        {
            var alerts = _notifyRepository.Get().Where(x => x.IsDeleted == false).ToList();

            alerts.ForEach(x =>
            {
                _notifyRepository.Remove(x);
            });
        }
    }
}
