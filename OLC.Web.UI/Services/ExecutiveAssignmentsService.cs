using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class ExecutiveAssignmentsService : IExecutiveAssignmentsService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ExecutiveAssignmentsService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<bool> DeleteExecutiveAssignmentsAsync(long id)
        {

            var url = Path.Combine("ExecutiveAssignments/DeleteExecutiveAssignmentsAsync", id.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);

        }

        public async Task<bool> InsertExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments)
        {
            return await _repositoryFactory.SendAsync<ExecutiveAssignments, bool>(HttpMethod.Post, "ExecutiveAssignments/InsertExecutiveAssignmentsAsync", executiveAssignments);

        }

        public async Task<bool> UpdateExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments)
        {
            return await _repositoryFactory.SendAsync<ExecutiveAssignments, bool>(HttpMethod.Post, "ExecutiveAssignments/InsertExecutiveAssignmentsAsync", executiveAssignments);
        }
    }
}
