using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public AccountTypeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteAccountTypeAsync(long accountTypeId)
        {

            var url = Path.Combine("AccountType/DeleteAccountTypeAsync", accountTypeId.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<AccountType>> GetAccountTypeAsync()
        {
            return await _repositoryFactory.SendAsync<List<AccountType>>(HttpMethod.Get, "AccountType/GetAccountTypeAsync");
        }

        public async Task<AccountType> GetAccountTypeByIdAsync(long accountTypeId)
        {
            var url = Path.Combine("AccountType/GetAccountTypeByIdAsync", accountTypeId.ToString());
            return await _repositoryFactory.SendAsync<AccountType>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertAccountTypeAsync(AccountType accountType)
        {
            return await _repositoryFactory.SendAsync<AccountType, bool>(HttpMethod.Post, "AccountType/InsertAccountTypeAsync", accountType);
        }

        public async Task<bool> UpdateAccountTypeAsync(AccountType accountType)
        {
            return await _repositoryFactory.SendAsync<AccountType, bool>(HttpMethod.Post, "AccountType/UpdateAccountTypeAsync", accountType);
        }
    }
}