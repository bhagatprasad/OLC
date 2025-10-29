using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IBankService
    {
        Task<Bank> GetBankByIdAsync(long bankId);
        Task<List<Bank>> GetBankAsync();  
        Task<bool> InsertBankAsync(Bank bank);
        Task<bool> UpdateBankAsync(Bank bank);
        Task<bool> DeleteBankAsync(long bankId);
    }
}

