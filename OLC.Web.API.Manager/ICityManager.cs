using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ICityManager
    {
        Task<List<City>> GetCitiesListAsync();
        Task<List<City>> GetCitiesByCountryAsync(long countryId);
        Task<List<City>> GetCitiesByStateAsync(long stateId);
        Task<City> GetCityByIdAsync(long cityId);
    }
}
