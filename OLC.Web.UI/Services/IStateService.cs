using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IStateService
    {
        Task<State> GetStateByStateAsync(long stateId);
        Task<List<State>> GetStatesByCountryAsync(long countryId);
        Task<List<State>> GetStatesListAsync();
    }
}
