using OLC.Web.UI.Models;
using System.Diagnostics;

namespace OLC.Web.UI.Services
{
    public class CityService : ICityService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public CityService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<City>> GetCitiesByCountryAsync(long countryId)
        {
            var url = Path.Combine("City/GetCitiesByCountryAsync",countryId.ToString());
            return await _repositoryFactory.SendAsync<List<City>>(HttpMethod.Get, url);
        }

        public async Task<List<City>> GetCitiesByStateAsync(long stateId)
        {
            var url = Path.Combine("City/GetCitiesByStateAsync",stateId.ToString());
            return await _repositoryFactory.SendAsync<List<City>>(HttpMethod.Get,url);
        }

        public async Task<City> GetCityByIdAsync(long cityId)
        {
            var url =Path.Combine("City/GetCityByIdAsync",cityId.ToString());   
            return await _repositoryFactory.SendAsync<City>(HttpMethod.Get, url);   
        }
        public async Task<List<City>> GetCitiesListAsync()
        {
            return await _repositoryFactory.SendAsync<List<City>>(HttpMethod.Get, "City/GetCitiesListAsync");
        }
    }
}
