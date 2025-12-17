namespace OLC.Web.API.Models
{
    public class Cryptocurrency
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Blockchain { get; set; }
        public string ContractAddress { get; set; }
        public int Decimals { get; set; }
        public bool IsStablecoin { get; set; }
        public decimal MinDepositAmount { get; set; }
        public decimal MinWithdrawalAmount { get; set; }
        public decimal WithdrawalFee { get; set; }
        public string IconUrl { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
