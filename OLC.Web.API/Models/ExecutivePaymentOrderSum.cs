namespace OLC.Web.API.Models
{
    public class ExecutivePaymentOrderSum
    {

        public decimal? TotalAmount { get; set; }
        public decimal? DepositedAmount { get; set; }
        public decimal? PlatformFee { get; set; }
        public decimal? SuccessAmount { get; set; }
        public decimal? FailedAmount { get; set; }
        public decimal? CancelledAmount { get; set; }
    }
}
