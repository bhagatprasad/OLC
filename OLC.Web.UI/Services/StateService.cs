using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class StateService:IStateService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public StateService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<State> GetStateByStateAsync(long stateId)
        {
            var url = Path.Combine("State/GetStateByStateAsync", stateId.ToString());
            return await _repositoryFactory.SendAsync<State>(HttpMethod.Get,url);
            
        }

        public async Task<List<State>> GetStatesByCountryAsync(long countryId)
        {
            var url = Path.Combine("State/GetStatesByCountryAsync", countryId.ToString());
            return await _repositoryFactory.SendAsync<List<State>>(HttpMethod.Get,url);
        }

        public async Task<List<State>> GetStatesListAsync()
        {
            return await _repositoryFactory.SendAsync<List<State>>(HttpMethod.Get, "State/GetStatesListAsync");
        }
    }
}
