
namespace OLC.Web.UI.Models
{
    public class ProcessPaymentStatus
    {
        public long? PaymentOrderId { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? PaymentMethod { get; set; }
        public long? OrderStatusId { get; set; }
        public long? PaymentStatusId { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
    }
}
