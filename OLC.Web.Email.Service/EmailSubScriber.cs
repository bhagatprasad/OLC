using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace OLC.Web.Email.Service
{
    public class EmailSubScriber : IEmailSubScriber
    {
        private readonly string _smtpServcer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _appPassword;
        public EmailSubScriber(IOptions<EmailConfig> emailConfig)
        {
            var coreConfigValue = emailConfig.Value;
            _smtpServcer = coreConfigValue.SmtpServer;
            _smtpPort= coreConfigValue.SmtpPort;
            _senderEmail= coreConfigValue.SenderEmail;
            _appPassword= coreConfigValue.AppPassword;
        }
        public bool SendEmailAsync(EmailRequest emailRequest)
        {
            try
            {
                var smtp = new SmtpClient(_smtpServcer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_senderEmail, _appPassword),
                    EnableSsl = true
                };

                var message = new MailMessage(
                    emailRequest.FromEmail,
                    emailRequest.ToEmail,
                    emailRequest.Subject,
                    emailRequest.Body
                );
                
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }
    }
}
