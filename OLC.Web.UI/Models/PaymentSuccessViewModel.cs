namespace OLC.Web.UI.Models
{
    public class PaymentSuccessViewModel
    {
        public string? SessionId { get; set; }
        public long? PaymentOrderId { get; set; }
        public string? OrderReference { get; set; }
        public string? PaymentIntentId { get; set; }
        public decimal? AmountTotal { get; set; }
        public string? Currency { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerName { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? Created { get; set; }
        public string? UserId { get; set; }
        public string? CreditCardId { get; set; }
        public string? BankAccountId { get; set; }
        public string? BillingAddressId { get; set; }
    }
}
