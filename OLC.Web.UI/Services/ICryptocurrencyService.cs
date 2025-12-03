using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ICryptocurrencyService
    {
        Task<Cryptocurrency> GetCryptocurrencyByIdAsync(long id);
        Task<List<Cryptocurrency>> GetAllCryptocurrenciesAsync();
        Task<bool> InserCryptocurrencyAsync(Cryptocurrency cryptocurrency);
        Task<bool> UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency);
    }
}
