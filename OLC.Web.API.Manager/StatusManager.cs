using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class StatusManager : IStatusManager
    {
        private readonly string connectionString;
        public StatusManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Status> GetStatusByIdAsync(long statusId)
        {
            Status status = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetStatusById]", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@statusId", statusId);

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dt = new DataTable();

                        sqlDataAdapter.Fill(dt);

                        connection.Close();

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                status = new Status();

                                status.Id = Convert.ToInt64(item["Id"]);
                                status.Name = item["Name"] != DBNull.Value ? item["Name"].ToString() : null;
                                status.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;
                                status.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                                status.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                                status.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                                status.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                                status.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                            }
                        }
                    }

                }


            }
            return status;
        }

        public async Task<List<Status>> GetStatusesAsync()
        {
            List<Status> statusList = new List<Status>();

            Status status = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetStatuses]", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dt = new DataTable();
                        sqlDataAdapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                status = new Status();

                                status.Id = Convert.ToInt64(item["Id"]);
                                status.Name = item["Name"] != DBNull.Value ? item["Name"].ToString() : null;
                                status.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;
                                status.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                                status.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                                status.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                                status.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                                status.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                                statusList.Add(status);
                            }
                        }
                    }

                }

                connection.Close();
            }


            return statusList;
        }

        public async Task<bool> SaveStatusAsync(Status status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertStatus]", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@name", status.Name);
                    sqlCommand.Parameters.AddWithValue("@code", status.Code);
                    sqlCommand.Parameters.AddWithValue("@createdBy", status.CreatedBy);
                    sqlCommand.ExecuteNonQuery();

                }
                connection.Close();
                return true;
            }
            return false;

        }

    }
}
