using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class CryptocurrencyService:ICryptocurrencyService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public CryptocurrencyService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<Cryptocurrency>> GetAllCryptocurrenciesAsync()
        {
            return await _repositoryFactory.SendAsync<List<Cryptocurrency>>(HttpMethod.Get, "Cryptocurrency/GetAllCryptocurrenciesAsync");
        }

        public async Task<Cryptocurrency> GetCryptocurrencyByIdAsync(long id)
        {
            var url = Path.Combine("Cryptocurrency/GetCryptocurrencyByIdAsync", id.ToString());
            return await _repositoryFactory.SendAsync<Cryptocurrency>(HttpMethod.Get, url);
        }

        public async Task<bool> InserCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            return await _repositoryFactory.SendAsync<Cryptocurrency, bool>(HttpMethod.Post, "Cryptocurrency/InserCryptocurrencyAsync", cryptocurrency);
        }

        public async Task<bool> UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            return await _repositoryFactory.SendAsync<Cryptocurrency, bool>(HttpMethod.Post, "Cryptocurrency/UpdateCryptocurrencyAsync", cryptocurrency);
        }
    }
}
