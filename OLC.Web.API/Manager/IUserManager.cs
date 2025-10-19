using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IUserManager
    {
        Task<List<UserAccount>> GetUserAccountsAsync();
    }
}
