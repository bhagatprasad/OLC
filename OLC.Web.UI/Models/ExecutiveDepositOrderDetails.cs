namespace OLC.Web.UI.Models
{
    public class ExecutiveDepositOrderDetails
    {
        public long? UserId { get; set; }
        public long? DepositOrderId { get; set; }
        public long? PaymentOrderId { get; set; }
        public long? PaymentReasonId { get; set; }

        public string DepositeReferance { get; set; }
        public string OrderReference { get; set; }

        public decimal? TotalAmount { get; set; }
        public decimal? DepositedAmount { get; set; }
        public decimal? PendingAmount { get; set; }

        public string UserEmail { get; set; }
        public string UserPhone { get; set; }

        public string CreditCardNumber { get; set; }

        public string DepositedTo { get; set; }
        public string BankAccount { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
    }
}
