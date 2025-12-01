namespace OLC.Web.UI.Models
{
    public class TransactionReward
    {
        public long Id { get; set; }

        public long PaymentOrderId { get; set; }

        public long UserId { get; set; }

        public long RewardConfigurationId { get; set; }

        public decimal TransactionAmount { get; set; }  // DECIMAL(18,6)

        public decimal RewardAmount { get; set; }       // DECIMAL(18,6)

        public decimal RewardRate { get; set; }         // DECIMAL(18,6)

        public string RewardStatus { get; set; } = "Pending"; // pending, credited, cancelled

        public long? CreditedToWalletId { get; set; }

        public DateTimeOffset? CreditedOn { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public bool IsActive { get; set; } = true;

        public long? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset ModifiedOn { get; set; } = DateTimeOffset.UtcNow;

        public long? ModifiedBy { get; set; }
    }
}
