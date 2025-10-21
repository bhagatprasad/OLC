using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ITransactionTypeManager
    {
        Task<List<TransactionType>> GetTransactionTypeAsync();
        Task<bool> InsertTransactionTypeAsync(TransactionType transactionType);
        Task<bool> UpdateTransactionTypeAsync(TransactionType transactionType);
        Task<bool> DeleteTransactionTypeAsync(long id);
        Task<TransactionType> GetTransactionTypeByIdAsync(long id);
    }
}
