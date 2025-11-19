using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class VerifyUserKycService : IVerifyUserKycService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public VerifyUserKycService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<List<VerifyUserKyc>> GetAllVerifyUserKycAsync()
        {
            return await _repositoryFactory.SendAsync<List<VerifyUserKyc>>(HttpMethod.Get, "VerifyUserKyc/GetAllVerifyUserKycAsync");
        }
    }
}
