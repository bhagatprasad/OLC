namespace OLC.Web.UI.Models
{
    public class PaymentOrder
    {
        public long Id { get; set; }
        public string OrderReference { get; set; }
        public long UserId { get; set; }
        public long? PaymentReasonId { get; set; }
        public decimal Amount { get; set; }
        public long TransactionFeeId { get; set; }
        public decimal PlatformFeeAmount { get; set; }
        public string FeeCollectionMethod { get; set; }
        public decimal TotalAmountToChargeCustomer { get; set; }
        public decimal TotalAmountToDepositToCustomer { get; set; }
        public decimal TotalPlatformFee { get; set; }
        public string Currency { get; set; }
        public long? CreditCardId { get; set; }
        public long? BankAccountId { get; set; }
        public long? BillingAddressId { get; set; }
        public long? OrderStatusId { get; set; }
        public long? PaymentStatusId { get; set; }
        public long? DepositStatusId { get; set; }
        public string? StripePaymentIntentId { get; set; }
        public string? StripePaymentChargeId { get; set; }
        public string? StripeDepositeIntentId { get; set; }
        public string? StripeDepositeChargeId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
