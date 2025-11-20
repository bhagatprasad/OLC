using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC.Web.Email.Service
{
    public interface IEmailSubScriber
    {
        bool SendEmailAsync(EmailRequest emailRequest);
    }
}
