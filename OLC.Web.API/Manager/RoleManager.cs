using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class RoleManager : IRoleManager
    {
        private readonly string connectionString;

        public RoleManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> InsertRoleAsync(Role role)
        {
            if (role != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertRoles]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@name", role.Name);
                sqlCommand.Parameters.AddWithValue("@code", role.Code);
                sqlCommand.Parameters.AddWithValue("@createdBy", role.CreatedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRoleAsync(long roleId)
        {
            if (roleId > 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteRoles]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", roleId);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<List<Role>> GetRolesListAsync()
        {
            List<Role> roles = new List<Role>();
            Role role = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetRoles]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    role = new Role();
                    role.Id = Convert.ToInt64(dr["Id"]);
                    role.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null;
                    role.Code = dr["Code"] != DBNull.Value ? dr["Code"].ToString() : null;
                    role.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    role.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    role.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    role.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    role.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : null;

                    roles.Add(role);
                }
            }
            return roles;
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            if (role != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateRoles]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", role.Id);
                sqlCommand.Parameters.AddWithValue("@name", role.Name);
                sqlCommand.Parameters.AddWithValue("@code", role.Code);
                sqlCommand.Parameters.AddWithValue("@isActive", role.IsActive);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", role.ModifiedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<Role> GetRoleByIdAsync(long roleId)
        {
            Role role = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetRolesById]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@roleId", roleId);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    role = new Role();
                    role.Id = Convert.ToInt64(dr["Id"]);
                    role.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null;
                    role.Code = dr["Code"] != DBNull.Value ? dr["Code"].ToString() : null;
                    role.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    role.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    role.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    role.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    role.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : null;
                }
            }
            return role;
        }
    }
}
