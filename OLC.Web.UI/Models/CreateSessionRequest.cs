namespace OLC.Web.UI.Models
{
    public class CreateSessionRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "usd";
        public string CustomerEmail { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}
