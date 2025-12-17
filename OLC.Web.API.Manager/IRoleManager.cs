using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface IRoleManager
    {
        Task<bool> InsertRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(long roleId);
        Task<Role> GetRoleByIdAsync(long roleId);
        Task<List<Role>> GetRolesListAsync();
    }
}
