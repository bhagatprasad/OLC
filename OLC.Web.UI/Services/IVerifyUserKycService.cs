using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IVerifyUserKycService
    {
        Task<List<VerifyUserKyc>> GetAllVerifyUserKycAsync();
    }
}
