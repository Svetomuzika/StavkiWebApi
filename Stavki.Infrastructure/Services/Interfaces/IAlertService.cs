using Stavki.Infrastructure.EF.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stavki.Infrastructure.Services.Interfaces
{
    public interface IAlertService
    {
        List<NotifyDomain> GetAlerts(int userId);

        void RemoveAlerts();
    }
}
