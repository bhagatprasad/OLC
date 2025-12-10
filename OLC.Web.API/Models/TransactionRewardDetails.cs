namespace OLC.Web.API.Models
{
    public class TransactionRewardDetails
    {
        public long Id { get; set; }
        public long WalletId { get; set; }
        public string PaymentOrderReferenceId { get; set; }
        public decimal? TotalEarned { get; set; }
        public decimal? TotalSpent { get; set; }
        public decimal? CurrentBalance { get; set; }
        public  decimal ChargeableAmount { get; set; }
        public decimal DepositableAmount { get; set; }
        public decimal? RewardAmount { get; set; }
        public string? AccountHolderName { get; set; }
        public long? CardNumber { get; set; }
        public long? AccountNumber { get; set; }
        public  long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
