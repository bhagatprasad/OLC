using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Base;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OLC.Web.Sms.Service
{
    public class SmsSubscriber : ISmsSubscriber
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;
        public SmsSubscriber(IOptions<SmsConfig> smsConfig)
        {
            var coreConfigValue = smsConfig.Value;
            _accountSid = coreConfigValue.AccountSid;
            _authToken = coreConfigValue.AuthToken;
            _fromNumber = coreConfigValue.FromNumber;
        }
        public bool SendSmsAsync(SmsRequest smsRequest)
        {
            string accountSid = _accountSid;
            string authToken = _authToken;
            string fromNumber = _fromNumber;  

          
            TwilioClient.Init(accountSid, authToken);

      
            string input = Console.ReadLine();
            List<string> phoneNumbers = new List<string>(input.Split(','));
            string message = Console.ReadLine();

            List<string> sids = new List<string>();

            foreach (var number in phoneNumbers)
            {
                var trimmed = number.Trim();

                var msg = MessageResource.Create(
                    body: message,
                    from: new PhoneNumber(fromNumber),
                    to: new PhoneNumber(trimmed)
                );

                Console.WriteLine($"SMS Sent To: {trimmed} | SID: {msg.Sid}");
                sids.Add(msg.Sid);
            }
            return true;

           
            Console.ReadLine();
        }
    }
    
}
