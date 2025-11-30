using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ITransactionRewardManager
    {
        Task<bool> InsertTransactionRewardAsync(TransactionReward transactionReward);
        Task<List<TransactionReward>> GetAllTransactionRewardsAsync();
        Task<List<TransactionReward>> GetAllTransactionRewardsByUserIdAsync(long userId);
        Task<TransactionReward> GetTransactionRewardByPaymentOrderIdAsync(long paymentOrderId);
    }
}
