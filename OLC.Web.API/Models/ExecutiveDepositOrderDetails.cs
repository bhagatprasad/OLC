namespace OLC.Web.API.Models
{
    public class ExecutiveDepositOrderDetails
    {
        public long? UserId { get; set; }
        public long? DepositOrderId { get; set; }
        public long?PaymentOrderId { get; set; }
        public long? PaymentReasonId { get; set; }  

        public string DepositeReferance { get; set; }
        public string PaymentReferance { get; set; }

        public decimal? ActualDepositeAmount { get; set; }
        public decimal? DepositeAmount { get; set; }
        public decimal? PendingDepositeAmount { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public string CreditCardNumber { get; set; }

        public string DepositedTo { get; set; }
        public string DepositedAccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
    }
}
