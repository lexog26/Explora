using Explora.Web.Configurations;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Explora.Web.Helpers
{
    public class EmailSender : IEmailSender
    {
        protected readonly SmtpConfiguration _smtpConfig;

        public EmailSender(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfig = smtpConfiguration.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_smtpConfig.Host, _smtpConfig.Port))
                {
                    var message = new MailMessage();
                    message.From = new MailAddress(_smtpConfig.From, _smtpConfig.Alias);
                    message.BodyEncoding = Encoding.UTF8;
                    message.To.Add(email);
                    message.Body = htmlMessage;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_smtpConfig.From, _smtpConfig.Password);
                    await client.SendMailAsync(message);
                }
            }
            catch { }
        }
    }
}
