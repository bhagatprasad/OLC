using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ITransactionTypeManager
    {
        Task<List<TransactionType>> GetTransactionTypeAsync();
        Task<TransactionType> insertTransactionTypeAsync(TransactionType transactionType);
        Task<TransactionType> updateTransactionTypeAsync(TransactionType transactionType);
        Task<bool> deleteTransactionTypeAsync(long id);
        Task<TransactionType> GetTransactionTypeByIdAsync(long id);
    }
}
