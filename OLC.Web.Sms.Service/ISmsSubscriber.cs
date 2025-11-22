namespace OLC.Web.Sms.Service
{
    public interface ISmsSubscriber
    {
        bool SendSmsAsync(SmsRequest smsRequest);
    }
}
