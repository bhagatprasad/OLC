using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ITransactionFeeManager
    {
        Task<List<TransactionFee>> GetTransactionFeesListAsync();
        Task<TransactionFee> GetTransactionFeeByIdAsync(long feeId);
    }
}
