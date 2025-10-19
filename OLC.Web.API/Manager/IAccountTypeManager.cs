using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IAccountTypeManager
    {
        Task<GetAccountType> GetAccountTypeByIdAsync(long Id);
        Task<List<GetAccountType>> GetAccountTypeAsync(long createdBy);
        Task<bool> InsertUserAccountTypeAsync(AccountType accountType);
        Task<bool> UpdateUserAccountTypeAsync(UpdateAccountType updateAccountType);
        Task<bool> DeleteUserAccoutntTypeAsync(long accountTypeId);

    }
}
