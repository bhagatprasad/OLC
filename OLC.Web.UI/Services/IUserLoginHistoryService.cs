using OLC.Web.UI.Models;
namespace OLC.Web.UI.Services
{
    public interface IUserLoginHistoryService
    {
        Task<List<UserLogInHistory>> GetAllUserLoginHistoryAsync();
        Task<List<UserLogInHistory>> GetAllUserActivityTodayAsync();
        Task<UserLogInHistory> GetUserLoginActivityByUserIdAsync(long userId);
    }
}
