using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IBankManager
    {
        Task<bool>InsertBankAsync(Bank bank);
        Task<bool> UpdateBankAsync(Bank bank);
        Task<bool> DeleteBankAsync(long bankId);
        Task<Bank> GetBankByIdAsync(long bankId);
        Task<List<Bank>> GetBanksListAsync();
    }
}
