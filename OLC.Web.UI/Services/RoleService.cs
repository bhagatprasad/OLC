using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public RoleService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _repositoryFactory.SendAsync<List<Role>>(HttpMethod.Get, "Role/GetRolesListAsync");
        }

        public async Task<Role> GetRoleByIdAsync(long roleId)
        {
            var url = Path.Combine("Role/GetRoleByIdAsync", roleId.ToString());
            return await _repositoryFactory.SendAsync<Role>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertRoleAsync(Role role)
        {
            return await _repositoryFactory.SendAsync<Role, bool>(HttpMethod.Post, "Role/InsertRoleAsync", role);
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            return await _repositoryFactory.SendAsync<Role, bool>(HttpMethod.Post, "Role/UpdateRoleAsync", role);
        }

        public async Task<bool> DeleteRoleAsync(long roleId)
        {
            var url = $"Role/DeleteRole?bankId={roleId}";
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

    }
}
