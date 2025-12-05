using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    



    public interface IUserLoginHistoryManager
    {
        Task<List<UserLoginHistory>> GetAllUserLoginHistoryAsync();
        Task<List<UserLoginHistory>> GetAllUserActivityTodayAsync();
        Task<List<UserLoginHistory>>GetUserLoginActivityByUserIdAsync(long userId);
    }
}
