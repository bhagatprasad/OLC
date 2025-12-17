using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ICryptocurrencyManager
    {
        Task<Cryptocurrency> GetCryptocurrencyByIdAsync(long id);
        Task<List<Cryptocurrency>> GetAllCryptocurrenciesAsync();
        Task<bool> InserCryptocurrencyAsync(Cryptocurrency cryptocurrency);
        Task<bool> UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency);
    }
}
