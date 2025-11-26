using Newtonsoft.Json;
using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public CountryService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<bool> DeleteCountryAsync(long countryId)
        {
            var url = Path.Combine("Country/DeleteCounryAsync", countryId.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _repositoryFactory.SendAsync<List<Country>>(HttpMethod.Get, "Country/GetCountriesListAsync");

        }

        public async Task<Country> GetCountryByCountryIdAsync(long countryId)
        {
            var url = Path.Combine("Country/GetCountryByIdAsync", countryId.ToString());
            return await _repositoryFactory.SendAsync<Country>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertCountryAsync(Country country)
        {
            return await _repositoryFactory.SendAsync<Country, bool>(HttpMethod.Post, "Country/InsertCountryAsync", country);
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            return await _repositoryFactory.SendAsync<Country, bool>(HttpMethod.Post, "Country/UpdateCountryAsync", country);
        }
    }
}
