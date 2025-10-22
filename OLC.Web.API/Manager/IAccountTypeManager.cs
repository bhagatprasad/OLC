using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAccountTypeManager
    {
        Task<AccountType> GetAccountTypeByIdAsync(long accountTypeId);
        Task<List<AccountType>> GetAccountTypeAsync();
        Task<bool> InsertUserAccountTypeAsync(AccountType accountType);
        Task<bool> UpdateUserAccountTypeAsync(AccountType accountType);
        Task<bool> DeleteUserAccoutntTypeAsync(long accountTypeId);

    }
}
