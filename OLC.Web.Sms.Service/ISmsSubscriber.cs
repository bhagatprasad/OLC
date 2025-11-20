using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC.Web.Sms.Service
{
   public interface ISmsSubscriber
    {
        bool SendSmsAsync(SmsRequest smsRequest);
    }
}
