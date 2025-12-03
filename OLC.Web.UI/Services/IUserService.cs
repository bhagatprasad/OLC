using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IUserService
    {
        Task<List<UserAccount>> GetUserAccountsAsync();
        Task<ApplicationUser> GetUserAccountAsync(long userId);
        Task<PreviewUserKycDocument> PreviewUserKycDocumentAsync(long userId);
        Task<ApplicationUser>UpdateUserPersonalInformationAsync(UserPersonalInformation userPersonalInformation);
    }
}
