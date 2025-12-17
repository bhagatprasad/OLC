using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class PriorityManager : IPriorityManager
    {
        private readonly string connectionString;

        public PriorityManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Priority> GetPriorityByIdAsync(long priorityId)
        {
            Priority getPriorityById = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetPriorityById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@priorityId", priorityId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getPriorityById = new Priority();

                    getPriorityById.Id = Convert.ToInt64(item["Id"]);

                    getPriorityById.Name = (item["Name"].ToString());

                    getPriorityById.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getPriorityById.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getPriorityById.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getPriorityById.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getPriorityById.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return getPriorityById;
        }

        public async Task<List<Priority>> GetPriorityAsync()
        {
            List<Priority> getPriorities = new List<Priority>();

            Priority getPriority = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetPriority]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getPriority = new Priority();

                    getPriority.Id = Convert.ToInt64(item["Id"]);

                    getPriority.Name = item["Name"].ToString();

                    getPriority.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getPriority.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getPriority.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getPriority.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getPriority.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getPriorities.Add(getPriority);
                }
            }
            return getPriorities;
        }

        public async Task<bool> InsertPriorityAsync(Priority priority)
        {
            if (priority != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertPriority]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@name", priority.Name);

                sqlCommand.Parameters.AddWithValue("@code", priority.Code);

                sqlCommand.Parameters.AddWithValue("@createdBy", priority.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePriorityAsync(Priority priority)
        {
            if (priority != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdatePriority]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", priority.Id);

                sqlCommand.Parameters.AddWithValue("@name", priority.Name);

                sqlCommand.Parameters.AddWithValue("@code", priority.Code);

                sqlCommand.Parameters.AddWithValue("@modifiedBy", priority.ModifiedBy);

                sqlCommand.Parameters.AddWithValue("@isActive", priority.IsActive);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> DeletePriorityAsync(long priorityId)
        {
            if (priorityId != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeletePriority]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@priorityId", priorityId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }

    }
}
