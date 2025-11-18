using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IPriorityService
    {
        Task<Priority> GetPriorityByIdAsync(long priorityId);
        Task<List<Priority>> GetPriorityAsync();
        Task<bool> InsertPriorityAsync(Priority priority);
        Task<bool> UpdatePriorityAsync(Priority priority);
        Task<bool> DeletePriorityAsync(long priorityId);
    }
}
