using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IUserKycManager
    {
        Task<bool> InsertUserKycAsync(UserKyc userKyc);
        Task<bool> ProcessUserKycAsync(UserKyc userKyc);
        Task<UserKyc> GetUserKycByUserIdAsync(long userId);
        Task<List<UserKyc>> GetAllUsersKycAsync();
        Task<bool> VerifyUserKycAsync(VerifyUserKyc verifyUserKyc);
    }
}
