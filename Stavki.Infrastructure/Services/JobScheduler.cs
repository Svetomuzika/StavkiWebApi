using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stavki.Infrastructure.Services
{
    public class JobScheduler
    {
        public static void Run()
        {
            RecurringJob.AddOrUpdate("DeletingJob", () => DeletingJob(), Cron.Daily, TimeZoneInfo.Local, "stavki");
            BackgroundJob.Schedule("stavki", () => SendDelayedMessagesJob(), TimeSpan.FromSeconds(60));
            BackgroundJob.Schedule("stavki", () => SendDelayedMessagesJob(), TimeSpan.FromSeconds(44));
        }

        public static void DeletingJob()
        {

        }

        public static void SendDelayedMessagesJob()
        {

        }
    }
}
