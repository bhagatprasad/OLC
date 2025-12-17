using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IExecutiveAssignmentsService
    {
        Task<bool> InsertExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments);
        Task<bool> UpdateExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments);
        Task<bool> DeleteExecutiveAssignmentsAsync(long id);
    }
}
