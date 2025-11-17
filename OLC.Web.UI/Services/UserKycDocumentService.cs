using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class UserKycDocumentService : IUserKycDocumentService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public UserKycDocumentService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<UserKycDocument>> GetAllUsersKycDocumentsAsync()
        {
            return await _repositoryFactory.SendAsync<List<UserKycDocument>>(HttpMethod.Get, "User/GetAllUsersKycDocuments");
        }

        public async Task<bool> UpdateUserKycDocumentAsync(UserKycDocument userKycDocument)
        {
            return await _repositoryFactory.SendAsync<UserKycDocument, bool>(HttpMethod.Post, "User/UpdateUserKycDocumentAsync");
        }

        public async Task<bool> UploadeUserKycDocumentAsync(UserKycDocument userKycDocument)
        {
            return await _repositoryFactory.SendAsync<UserKycDocument, bool>(HttpMethod.Post, "User/UploadeUserKycDocumentAsync");
        }
    }
}
