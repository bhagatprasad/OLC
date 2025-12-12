using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAccountTypeManager
    {
        Task<AccountType> GetAccountTypeByIdAsync(long accountTypeId);
        Task<List<AccountType>> GetAccountTypeAsync();
        Task<bool> InsertAccountTypeAsync (AccountType accountType);
        Task<bool> UpdateAccountTypeAsync(AccountType accountType);
        Task<bool> DeleteAccoutntTypeAsync(long accountTypeId);
        Task<bool> InsertAccountTypeAsyncc(AccountType accountType);

    }
}
