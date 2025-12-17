namespace OLC.Web.API.Models
{
    public class PushPaymentOrderIntoQue
    {
        public List<long> PaymentOrderIds { get; set; }
        public long UserId { get; set; }
        public long ExecutiveId { get; set; }
        public long AssignedBy { get; set; }
        public DateTimeOffset? AssignedAt { get; set; }
    }
}
