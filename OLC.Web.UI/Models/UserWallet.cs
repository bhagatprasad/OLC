namespace OLC.Web.UI.Models
{
    public class UserWallet
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string WalletId { get; set; }
        public string WalletType { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal TotalEarned { get; set; }
        public decimal TotalSpent { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
