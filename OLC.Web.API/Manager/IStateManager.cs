using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IStateManager
    {
        Task<State> GetStateByStateAsync (long stateId);
        Task<List<State>> GetStatesByCountryAsync (long countryId);
        Task<List<State>> GetStatesListAsync ();
    }
}
