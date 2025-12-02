using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IUserManager
    {
        Task<List<UserAccount>> GetUserAccountsAsync();
        Task<ApplicationUser> GetUserAccountAsync(long userId);
        Task<bool>UpdateUserPersonalInformationAsync(UserPersonalInformation userPersonalInformation);
    }
}
