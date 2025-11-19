using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IVerifyUserKycManager
    {
        Task<List<VerifyUserKyc>> GetAllVerifyUserKycAsync();
    }
}
