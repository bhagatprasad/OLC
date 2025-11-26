namespace OLC.Web.Sms.Service
{
    public class SmsRequest
    {
        public string Message { get; set; }
        public List<string> CustomerPhoneNumbers { get; set; }
    }
}
