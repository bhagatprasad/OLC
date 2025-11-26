using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IPriorityManager
    {
        Task<Priority> GetPriorityByIdAsync(long priorityId);
        Task<List<Priority>> GetPriorityAsync();
        Task<bool> InsertPriorityAsync(Priority priority);
        Task<bool> UpdatePriorityAsync(Priority priority);
        Task<bool> DeletePriorityAsync(long priorityId);
        
    }
}
