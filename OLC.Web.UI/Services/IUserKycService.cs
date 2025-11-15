using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IUserKycService
    {
        Task<bool> InsertUserKycAsync(UserKyc userKyc);
        Task<bool> ProcessUserKycAsync(UserKyc userKyc);
        Task<UserKyc> GetUserKycByUserIdAsync(long userId);
        Task<List<UserKyc>> GetAllUsersKycAsync();
    }
}
