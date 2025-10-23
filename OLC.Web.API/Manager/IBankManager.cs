using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IBankManager
    {
        Task<bool>InsertBankAsync(Bank bank);
        Task<bool> UpdateBankAsync(Bank bank);
        Task<bool> DeleteBankAsync(long Id);
        Task<Bank> GetBankByIdAsync(long id);
        Task<List<Bank>> GetBankAsync();

    }
}
