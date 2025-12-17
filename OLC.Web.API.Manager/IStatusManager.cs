using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IStatusManager
    {
        Task<bool> SaveStatusAsync(Status status);

        Task<Status> GetStatusByIdAsync(long statusId);

        Task<List<Status>> GetStatusesAsync();
    }
}
