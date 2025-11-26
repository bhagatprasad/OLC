using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IUserKycDocumentManager
    {
        Task<bool> UploadeUserKycDocumentAsync(UserKycDocument userKycDocument);
        Task<bool> UpdateUserKycDocumentAsync(UserKycDocument userKycDocument);
        Task<List<UserKycDocument>> GetAllUsersKycDocumentsAsync();
        Task<UserKycDocument> GetUserKycDocumentByUserAsync(long userId);
    }
}
