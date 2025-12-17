namespace OLC.Web.Email.Service
{
    public interface IEmailSubScriber
    {
        bool SendEmailAsync(EmailRequest emailRequest);
    }
}
