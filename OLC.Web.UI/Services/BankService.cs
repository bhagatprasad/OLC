using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class BankService : IBankService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public BankService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteBankAsync(long bankId)
        {
            
            var url = $"Bank/DeleteBank?bankId={bankId}";
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<Bank>> GetBankAsync()  
        {
            return await _repositoryFactory.SendAsync<List<Bank>>(HttpMethod.Get, "Bank/GetBanksListAsync");
        }

        public async Task<Bank> GetBankByIdAsync(long bankId)
        {
            var url = Path.Combine("Bank/GetBankByIdAsync", bankId.ToString());
            return await _repositoryFactory.SendAsync<Bank>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertBankAsync(Bank bank)
        {
            return await _repositoryFactory.SendAsync<Bank, bool>(HttpMethod.Post, "Bank/InsertBankAsync", bank);
        }

        public async Task<bool> UpdateBankAsync(Bank bank)
        {
            return await _repositoryFactory.SendAsync<Bank, bool>(HttpMethod.Post, "Bank/UpdateBankAsync", bank);
        }
    }
}
