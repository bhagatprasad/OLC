using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IExecutiveAssignmentsManager
    {
        Task<bool> InsertExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments);
        Task<bool> UpdateExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments);
    }
}
