namespace OLC.Web.UI.Models
{
    public class ProcessPaymentOrder
    {
        public long? PaymentOrderId { get; set; }
        public long? OrderStatusId { get; set; }
        public long? PaymentStatusId { get; set; }
        public long? DepositeStatusId { get; set; }
        public long? CreatedBy { get; set; }
        public string? Description { get; set; }
    }
}
