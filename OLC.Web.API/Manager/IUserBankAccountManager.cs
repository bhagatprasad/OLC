using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IUserBankAccountManager
    {
        Task<UserBankAccount> GetUserBankAccountByIdAsync(long id);
        Task<List<UserBankAccount>> GetAllUserBankAccountByUserIdAsync(long userId);
        Task<List<UserBankAccount>> GetAllUserBankAccountsAsync();
        Task<bool> InsertUserBankAccountAsync(UserBankAccount userBankAccount);
        Task<bool> UpdateUserBankAccountAsync(UserBankAccount userBankAccount);
        Task<bool> DeleteUserBankAccountAsync(long id);
    }
}
