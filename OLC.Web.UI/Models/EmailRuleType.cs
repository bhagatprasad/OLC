namespace OLC.Web.UI.Models
{
    public class EmailRuleType
    {
        public long Id { get; set; }
        public string RuleCode { get; set; }
        public string RuleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
    }
}
