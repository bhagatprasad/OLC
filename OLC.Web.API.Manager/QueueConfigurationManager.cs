using OLC.Web.API.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;


namespace OLC.Web.API.Manager
{
    public class QueueConfigurationManager : IQueueConfigurationManager
    {
        private readonly string connectionString;

        public QueueConfigurationManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> DeleteQueueConfigurationAsync(long id)
        {
            if (id != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteQueueConfiguration]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queueConfigurationId", id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<List<QueueConfiguration>> GetAllQueueConfigurationsAsync()
        {
            List<QueueConfiguration> queueConfigurations = new List<QueueConfiguration>();
            QueueConfiguration queueConfiguration = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllQueueConfigurations]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    queueConfiguration = new QueueConfiguration();

                    queueConfiguration.Id = Convert.ToInt64(row["Id"]);

                    queueConfiguration.Key = row["Key"] != DBNull.Value ? row["Key"].ToString() : null;

                    queueConfiguration.Value = row["Value"] != DBNull.Value ? row["Value"].ToString() : null;
                    queueConfiguration.DataType = row["DataType"] != DBNull.Value ? row["DataType"].ToString() : null;

                    queueConfiguration.Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null;

                    queueConfiguration.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;

                    queueConfiguration.CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null;

                    queueConfiguration.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;

                    queueConfiguration.ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null;

                    queueConfiguration.IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null;

                    queueConfigurations.Add(queueConfiguration);
                }
            }
            return queueConfigurations;
        }

        public async Task<QueueConfiguration> GetQueueConfigurationByIdAsync(long id)
        {
            QueueConfiguration queueConfiguration = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetQueueConfigurationById]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@QueueConfigurationId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    queueConfiguration = new QueueConfiguration();

                    queueConfiguration.Id = Convert.ToInt64(row["Id"]);

                    queueConfiguration.Key = row["Key"] != DBNull.Value ? row["Key"].ToString() : null;

                    queueConfiguration.Value = row["Value"] != DBNull.Value ? row["Value"].ToString() : null;

                    queueConfiguration.DataType = row["DataType"] != DBNull.Value ? row["DataType"].ToString() : null;

                    queueConfiguration.Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null;

                    queueConfiguration.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;

                    queueConfiguration.CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null;

                    queueConfiguration.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;

                    queueConfiguration.ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null;

                    queueConfiguration.IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null;
                }
            }
            return queueConfiguration;
        }

        public async Task<bool> SaveQueueConfigurationAsync(QueueConfiguration queueConfiguration)
        {
            if (queueConfiguration != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspSaveQueueConfiguration]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Key", queueConfiguration.Key);
                sqlCommand.Parameters.AddWithValue("@Value", queueConfiguration.Value);
                sqlCommand.Parameters.AddWithValue("@DataType", queueConfiguration.DataType);
                sqlCommand.Parameters.AddWithValue("@Description", queueConfiguration.Description);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", queueConfiguration.CreatedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateQueueConfigurationAsync(QueueConfiguration queueConfiguration)
        {
            if (queueConfiguration != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateQueueConfiguration]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", queueConfiguration.Id);
                sqlCommand.Parameters.AddWithValue("@key", queueConfiguration.Key);
                sqlCommand.Parameters.AddWithValue("@value", queueConfiguration.Value);
                sqlCommand.Parameters.AddWithValue("@dataType", queueConfiguration.DataType);
                sqlCommand.Parameters.AddWithValue("@description", queueConfiguration.Description);
                sqlCommand.Parameters.AddWithValue("@IsActive", queueConfiguration.IsActive);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", queueConfiguration.ModifiedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}
