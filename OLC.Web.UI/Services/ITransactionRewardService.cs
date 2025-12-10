using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ITransactionRewardService
    {
        Task<bool> InsertTransactionRewardAsync(TransactionReward transactionReward);
        Task<List<TransactionReward>> GetAllTransactionRewardsAsync();
        Task<List<TransactionReward>> GetAllTransactionRewardsByUserIdAsync(long userId);
        Task<TransactionReward> GetTransactionRewardByPaymentOrderIdAsync(long paymentOrderId);
        Task<List<TransactionRewardDetails>> GetAllExecutiveTransactionRewardDetailsAsync();
    }
}
