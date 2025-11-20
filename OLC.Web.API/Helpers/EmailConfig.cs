namespace OLC.Web.API.Helpers
{
    public class EmailConfig
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string AppPassword { get; set; }
    }
}
