using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OLC.Web.Email.Service
{
    public class EmailSubScriber : IEmailSubScriber
    {
        public bool SendEmailAsync(EmailRequest emailRequest)
        {
            try
            {
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("Company.", "bgyl djba qqvs fssj"),
                    EnableSsl = true
                };

                var message = new MailMessage(
                    emailRequest.FromEmail,
                    emailRequest.ToEmail,
                    emailRequest.Subject,
                    emailRequest.Body
                );


                smtp.Send(message);

                Console.WriteLine("Email Sent Successfully!");

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
