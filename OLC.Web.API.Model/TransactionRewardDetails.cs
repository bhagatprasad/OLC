namespace OLC.Web.API.Models
{
    public class TransactionRewardDetails
    {        
            public string UserId { get; set; }           
            public string WalletId { get; set; }       
            public string PaymentOrderReferenceId { get; set; }

            public decimal? TotalEarned { get; set; }
            public decimal? TotalSpent { get; set; }
            public decimal? CurrentBalance { get; set; }

            public decimal ChargeableAmount { get; set; }
            public decimal DepositableAmount { get; set; }
            public decimal? RewardAmount { get; set; }

            public string? AccountHolderName { get; set; }

            public string? CardNumber { get; set; }     
            public string? AccountNumber { get; set; }  

            public string? CreatedBy { get; set; }       
            public DateTimeOffset? CreatedOn { get; set; }

            public bool? IsActive { get; set; }
        
    }
}
