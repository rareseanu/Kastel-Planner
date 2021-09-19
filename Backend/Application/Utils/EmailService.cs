using Domain.Configurations;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Application.Utils
{
    public interface IEmailService
    {
        void Send(string to, string subject, string body, string from = null);
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpConfig _smtpConfig;

        public EmailService(IOptions<SmtpConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        public void Send(string to, string subject, string body, string from = null)
        {
            // create message
            var email = new MailMessage();
            email.From = new MailAddress(from ?? _smtpConfig.From);
            email.To.Add(new MailAddress(to));
            email.Subject = subject;
            email.Body = body;

            // send email
            using var smtp = new SmtpClient(_smtpConfig.Host, int.Parse(_smtpConfig.Port));
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_smtpConfig.Username, _smtpConfig.Password);

            smtp.Send(email);
        }
    }
}