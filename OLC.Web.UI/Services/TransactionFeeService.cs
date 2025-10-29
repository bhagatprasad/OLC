using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class TransactionFeeService : ITransactionFeeService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public TransactionFeeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public Task<bool> DeleteTransactionFeeAsync(long TransactionFeeId)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionFee> GetTransactionFeeByIdAsync(long transactionFeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TransactionFee>> GetTransactionFeesListAsync()
        {
            return await _repositoryFactory.SendAsync<List<TransactionFee>>(HttpMethod.Get, "TransactionFee/GetTransactionFeesListAsync");
        }

        public Task<bool> InsertTransactionFeeAsync(TransactionFee transactionFee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTransactionFeeAsync(TransactionFee transactionFee)
        {
            throw new NotImplementedException();
        }
    }
}
