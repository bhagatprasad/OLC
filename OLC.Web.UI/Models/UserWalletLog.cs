namespace OLC.Web.UI.Models
{
    public class UserWalletLog
    {
        public long Id { get; set; }

        public long? WalletId { get; set; }
        public long? UserId { get; set; }

        public decimal? Amount { get; set; }

        public string? TransactionType { get; set; }

        public string? Description { get; set; }

        public string? ReferenceId { get; set; }

        public string? Currency { get; set; }

        public bool? IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }
    }
}
