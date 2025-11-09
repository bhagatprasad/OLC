using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ITransactionTypeService
    {
        Task<bool> DeleteTransactionTypeAsync(long transactionTypeId);
        Task<List<TransactionType>> GetTransactionTypeAsync();
        Task<TransactionType> GetTransactionTypeByIdAsync(long transactionTypeId);
        Task<bool> InsertTransactionTypeAsync(TransactionType transactionType);
        Task<bool> UpdateTransactionTypeAsync(TransactionType transactionType);
        Task<bool> ActivateTransactionTypeAsync(TransactionType transactionType);
    }
}
