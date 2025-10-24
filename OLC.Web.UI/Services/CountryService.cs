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
        public Task<bool> DeleteCountryAsync(long countryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _repositoryFactory.SendAsync<List<Country>>(HttpMethod.Get, "Country/GetCountriesListAsync");

        }

        public Task<Country> GetCountryByCountryIdAsync(long countryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertCountryAsync(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCountryAsync(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
