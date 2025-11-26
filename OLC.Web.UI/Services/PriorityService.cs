using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class PriorityService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public PriorityService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeletePriorityAsync(long priorityId)
        {
            var url = Path.Combine("Priority/DeletePriorityAsync", priorityId.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<Priority>> GetPriorityAsync()
        {
            return await _repositoryFactory.SendAsync<List<Priority>>(HttpMethod.Get, "Priority/GetPriorityAsync");
        }

        public async Task<Priority> GetPriorityByIdAsync(long priorityId)
        {
            var url = Path.Combine("Priority/GetPriorityByIdAsync", priorityId.ToString());
            return await _repositoryFactory.SendAsync<Priority>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertPriorityAsync(Priority priority)
        {
            return await _repositoryFactory.SendAsync<Priority, bool>(HttpMethod.Post, "Priority/InsertPriorityAsync", priority);
        }

        public async Task<bool> UpdatePriorityAsync(Priority priority)
        {
            return await _repositoryFactory.SendAsync<Priority, bool>(HttpMethod.Post, "Priority/UpdatePriorityAsync", priority);
        }
    }
}
