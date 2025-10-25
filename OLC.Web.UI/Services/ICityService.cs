using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ICityService
    {
        Task<List<City>> GetCitiesListAsync();
        Task<List<City>> GetCitiesByCountryAsync(long countryId);
        Task<List<City>> GetCitiesByStateAsync(long stateId);
        Task<City> GetCityByIdAsync(long cityId);
    }
}
