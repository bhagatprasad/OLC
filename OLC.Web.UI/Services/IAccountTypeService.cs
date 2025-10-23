using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IAccountTypeService
    {
        Task<AccountType> GetAccountTypeByIdAsync(long accountTypeId);
        Task<List<AccountType>> GetAccountTypeAsync();
        Task<bool> InsertAccountTypeAsync(AccountType accountType);
        Task<bool> UpdateAccountTypeAsync(AccountType accountType);
        Task<bool> DeleteAccoutntTypeAsync(long accountTypeId);
    }
}
