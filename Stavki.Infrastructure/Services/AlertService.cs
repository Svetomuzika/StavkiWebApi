using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
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

        public void SendEmailAlert(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Тетра Транс", "tetratransbot@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("tetratransbot@gmail.com", "fltmxwjupebgxufn");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
