using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ICountryManager
    {
        Task<Country> GetCountryByCountryIdAsync(long countryId);
        Task<List<Country>> GetAllCountriesAsync();
        Task<bool> InsertCountryAsync(Country country);
        Task<bool> UpdateCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(long countryId);
    }
}
