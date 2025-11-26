using OLC.Web.UI.Models;
using Stripe;

namespace OLC.Web.UI.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public BankAccountService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> ActivateBankAccountAsync(UserBankAccount userBankAccount)
        {
            return await _repositoryFactory.SendAsync<UserBankAccount, bool>(HttpMethod.Post, "BankAccount,ActivateBankAccountAsync", userBankAccount);
        }

        public async Task<bool> DeleteUserBankAccountAsync(long id)
        {
            var url = Path.Combine("BankAccount/DeleteUserBankAccount", id.ToString());

            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<UserBankAccount>> GetAllUserBankAccountByUserIdAsync(long userId)
        {
            var url = Path.Combine("BankAccount/GetUserBankAccountsByUserId", userId.ToString());

            return await _repositoryFactory.SendAsync<List<UserBankAccount>>(HttpMethod.Get, url);
        }

        public async Task<List<UserBankAccount>> GetAllUserBankAccountsAsync()
        {
            var url = "BankAccount/GetAllUserBankAccounts";

            return await _repositoryFactory.SendAsync<List<UserBankAccount>>(HttpMethod.Get, url);
        }

        public async Task<UserBankAccount> GetUserBankAccountByIdAsync(long id)
        {
            var url = Path.Combine("BankAccount/GetUserBankAccountByIdAsync", id.ToString());

            return await _repositoryFactory.SendAsync<UserBankAccount>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertUserBankAccountAsync(UserBankAccount userBankAccount)
        {
            return await _repositoryFactory.SendAsync<UserBankAccount, bool>(HttpMethod.Post, "BankAccount/InsertUserBankAccount", userBankAccount);
        }

        public async Task<bool> UpdateUserBankAccountAsync(UserBankAccount userBankAccount)
        {
            return await _repositoryFactory.SendAsync<UserBankAccount, bool>(HttpMethod.Post, "BankAccount/UpdateUserBankAccount", userBankAccount);
        }
    }

}
