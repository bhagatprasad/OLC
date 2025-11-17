using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IUserKycDocumentService
    {
        Task<bool> UploadeUserKycDocumentAsync(UserKycDocument userKycDocument);
        Task<bool> UpdateUserKycDocumentAsync(UserKycDocument userKycDocument);
        Task<List<UserKycDocument>> GetAllUsersKycDocumentsAsync();
    }
}
