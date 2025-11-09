using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();
        Task<Role> GetRoleByIdAsync(long roleId);
        Task<bool> InsertRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(long roleId);
    }
}
