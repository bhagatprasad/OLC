namespace OLC.Web.UI.Models
{
    public class RewardConfiguration
    {
        public long? Id { get; set; }
        public string? RewardName { get; set; }
        public string? RewardType { get; set; }
        public decimal? RewardValue { get; set; }
        public decimal? MinimumTransactionAmount { get; set; }
        public decimal? MaximumReward { get; set; }
        public bool? IsActive { get; set; }
        public DateTimeOffset? ValidFrom { get; set; }
        public DateTimeOffset? ValidTo { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedOn { get; set; }
        public DateTimeOffset? ModifiedBy { get; set; }
    }
}





