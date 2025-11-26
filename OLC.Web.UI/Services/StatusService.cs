using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class StatusService : IStatusService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public StatusService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteStatusAsync(long statusId)
        {
            var url = Path.Combine("Status/DeleteStatusAsync", statusId.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<Status>> GetStatusAsync()
        {
            return await _repositoryFactory.SendAsync<List<Status>>(HttpMethod.Get, "Status/GetStatusesAsync");
        }

        public async Task<Status> GetStatusByIdAsync(long statusId)
        {
            var url = Path.Combine("Status/GetStatusByIdAsync", statusId.ToString());
            return await _repositoryFactory.SendAsync<Status>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertStatusAsync(Status status)
        {
            return await _repositoryFactory.SendAsync<Status, bool>(HttpMethod.Post, "Status/SaveStatusAsync", status);
        }

        public async Task<bool> UpdateStatusAsync(Status status)
        {
            return await _repositoryFactory.SendAsync<Status, bool>(HttpMethod.Post, "Status/UpdateStatusAsync", status);
        }
    }
}
