using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ITransactionFeeManager
    {
        Task<List<TransactionFee>> GetTransactionFeesListAsync();
        Task<TransactionFee> GetTransactionFeeByIdAsync(long transactionFeeId);
        Task<bool> UpdateTransactionFeeAsync(TransactionFee transactionFee);
        Task<bool> InsertTransactionFeeAsync(TransactionFee transactionFee);
        Task<bool> DeleteTransactionFeeAsync(long TransactionFeeId);
    }

}
