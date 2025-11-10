using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public TransactionTypeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteTransactionTypeAsync(long transactionTypeId)
        {
            var url = Path.Combine("TransactionType/DeleteTransactionTypeAsync", transactionTypeId.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<TransactionType>> GetTransactionTypeAsync()
        {
            return await _repositoryFactory.SendAsync<List<TransactionType>>(HttpMethod.Get, "TransactionType/GetTransactionTypeAsync");
        }

        public async Task<TransactionType> GetTransactionTypeByIdAsync(long transactionTypeId)
        {
            var url = Path.Combine("TransactionType/GetTransactionTypeByIdAsync", transactionTypeId.ToString());
            return await _repositoryFactory.SendAsync<TransactionType>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertTransactionTypeAsync(TransactionType transactionType)
        {
            return await _repositoryFactory.SendAsync<TransactionType, bool>(HttpMethod.Post, "TransactionType/InsertTransactionTypeAsync", transactionType);
        }

        public async Task<bool> UpdateTransactionTypeAsync(TransactionType transactionType)
        {
            return await _repositoryFactory.SendAsync<TransactionType, bool>(HttpMethod.Post, "TransactionType/UpdateTransactionTypeAsync", transactionType);
        }

        public async Task<bool> ActivateTransactionTypeAsync(TransactionType transactionType)
        {
            return await _repositoryFactory.SendAsync<TransactionType, bool>(HttpMethod.Post, "TransactionType/ActivateTransactionTypeAsync", transactionType);
        }
    }
}
