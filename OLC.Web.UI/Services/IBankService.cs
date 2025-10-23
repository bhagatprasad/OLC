using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IBankService
    {
        Task<bool> DeleteBankAsync(long bankId);
        Task<List<Bank>> GetBanksListAsync();
        Task<Bank> GetBankByIdAsync(long bankId);
        Task<bool> InsertBankAsync(Bank bank);
        Task<bool> UpdateBankAsync(Bank bank);
    }
}
