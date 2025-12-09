using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class TransactionRewardService : ITransactionRewardService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public TransactionRewardService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public Task<List<TransactionRewardDetails>> GetAllExecutiveTransactionRewardDetailsAsync()
        {
           return _repositoryFactory.SendAsync<List<TransactionRewardDetails>>(HttpMethod.Get, "TransactionReward/GetAllExecutiveTransactionRewardDetailsAsync");
        }

        public async Task<List<TransactionReward>> GetAllTransactionRewardsAsync()
        {
            return await _repositoryFactory.SendAsync<List<TransactionReward>>(HttpMethod.Get, "TransactionReward/GetAllTransactionRewardsAsync");
        }

        public async Task<List<TransactionReward>> GetAllTransactionRewardsByUserIdAsync(long userId)
        {
            var url = Path.Combine("TransactionReward/GetAllTransactionRewardsByUserIdAsync",userId.ToString());
            return await _repositoryFactory.SendAsync<List<TransactionReward>>(HttpMethod.Get, url);
        }

        public async Task<TransactionReward> GetTransactionRewardByPaymentOrderIdAsync(long paymentOrderId)
        {
            var url = Path.Combine("TransactionReward/GetTransactionRewardByPaymentOrderIdAsync", paymentOrderId.ToString());
            return await _repositoryFactory.SendAsync<TransactionReward>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertTransactionRewardAsync(TransactionReward transactionReward)
        {
            return await _repositoryFactory.SendAsync<TransactionReward,bool>(HttpMethod.Post, "TransactionReward/InsertTransactionRewardAsync", transactionReward);
        }
    }
}
