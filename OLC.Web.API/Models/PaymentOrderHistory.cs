namespace OLC.Web.API.Models
{
    public class PaymentOrderHistory
    {
        public int Id { get; set; }
        public int? PaymentOrderId { get; set; }
        public int? StatusId { get; set; }
        public string? Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; } 
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
