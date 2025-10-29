using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ITransactionFeeService
    {
        Task<List<TransactionFee>> GetTransactionFeesListAsync();
        Task<TransactionFee> GetTransactionFeeByIdAsync(long transactionFeeId);
        Task<bool> UpdateTransactionFeeAsync(TransactionFee transactionFee);
        Task<bool> InsertTransactionFeeAsync(TransactionFee transactionFee);
        Task<bool> DeleteTransactionFeeAsync(long TransactionFeeId);
    }
}
