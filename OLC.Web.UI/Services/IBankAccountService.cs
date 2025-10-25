using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IBankAccountService
    {
        Task<UserBankAccount> GetUserBankAccountByIdAsync(long id);
        Task<List<UserBankAccount>> GetAllUserBankAccountByUserIdAsync(long userId);
        Task<List<UserBankAccount>> GetAllUserBankAccountsAsync();
        Task<bool> InsertUserBankAccountAsync(UserBankAccount userBankAccount);
        Task<bool> UpdateUserBankAccountAsync(UserBankAccount userBankAccount);
        Task<bool> DeleteUserBankAccountAsync(long id);
    }
}
