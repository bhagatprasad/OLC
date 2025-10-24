using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ICountryService
    {
        Task<Country> GetCountryByCountryIdAsync(long countryId);
        Task<List<Country>> GetAllCountriesAsync();
        Task<bool> InsertCountryAsync(Country country);
        Task<bool> UpdateCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(long countryId);
    }
}
