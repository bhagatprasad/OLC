using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

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

            List<string> sids = new List<string>();

            foreach (var number in smsRequest.CustomerPhoneNumbers)
            {
                var trimmed = number.Trim();

                var msg = MessageResource.Create(
                    body: smsRequest.Message,
                    from: new PhoneNumber(fromNumber),
                    to: new PhoneNumber(trimmed)
                );
              
                sids.Add(msg.Sid);
            }
            return true;
        }
    }

}
