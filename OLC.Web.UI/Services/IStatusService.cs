using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IStatusService
    {
        Task<bool> DeleteStatusAsync(long statusId);
        Task<List<Status>> GetStatusAsync();
        Task<Status> GetStatusByIdAsync(long statusId);
        Task<bool> InsertStatusAsync(Status status);
        Task<bool> UpdateStatusAsync(Status status);
    }
}
