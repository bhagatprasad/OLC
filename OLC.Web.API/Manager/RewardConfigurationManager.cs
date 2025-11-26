using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class RewardConfigurationManager : IRewardConfigurationManager
    {
        private readonly string _connectionString;
        public RewardConfigurationManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> DeleteRewardConfigurationAsync(long Id)
        {
            if (Id > 0)
            {
                SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteRewardConfiguration]", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Id", Id);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<List<RewardConfiguration>> GetAllRewardConfigurationsAsync()
        {
            List<RewardConfiguration> rewardConfigurations = new List<RewardConfiguration>();

            RewardConfiguration rewardConfiguration = null;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllRewardConfigurations]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            da.Fill(dt);

            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    rewardConfiguration = new RewardConfiguration();
                    rewardConfiguration.Id = Convert.ToInt64(dr["Id"]);
                    rewardConfiguration.RewardName = dr["RewardName"] != DBNull.Value ? dr["RewardName"].ToString() : null;
                    rewardConfiguration.RewardType = dr["RewardType"] != DBNull.Value ? dr["RewardType"].ToString() : null;
                    rewardConfiguration.RewardValue = dr["RewardValue"] != DBNull.Value ? Convert.ToDecimal(dr["RewardValue"]) : 0;
                    rewardConfiguration.MinimumTransactionAmount = dr["MinimumTransactionAmount"] != DBNull.Value ? Convert.ToDecimal(dr["MinimumTransactionAmount"]) : 0;
                    rewardConfiguration.MaximumReward = dr["MaximumReward"] != DBNull.Value ? Convert.ToDecimal(dr["MaximumReward"]) : null;
                    rewardConfiguration.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : false;
                    rewardConfiguration.ValidFrom = dr["ValidFrom"] != DBNull.Value ? (DateTimeOffset)dr["ValidFrom"] : null;
                    rewardConfiguration.ValidTo = dr["ValidTo"] != DBNull.Value ? (DateTimeOffset)dr["ValidTo"] : null;
                    rewardConfiguration.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    rewardConfiguration.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    rewardConfiguration.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    rewardConfiguration.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    rewardConfigurations.Add(rewardConfiguration);

                }
            }
            return rewardConfigurations;
        }

        public async Task<RewardConfiguration> GetRewardConfigurationsByIdAsync(long Id)
        {
            RewardConfiguration rewardConfiguration = null;

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetRewardConfigurationDetailsById]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", Id);

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    rewardConfiguration = new RewardConfiguration();

                    rewardConfiguration.Id = Convert.ToInt64(item["Id"]);
                    rewardConfiguration.RewardName = Convert.ToString(item["RewardName"]);
                    rewardConfiguration.RewardType = Convert.ToString(item["RewardType"]);
                    rewardConfiguration.RewardValue = Convert.ToDecimal(item["RewardValue"]);
                    rewardConfiguration.MinimumTransactionAmount = Convert.ToDecimal(item["MinimumTransactionAmount"]);
                    rewardConfiguration.MaximumReward = item["MaximumReward"] != DBNull.Value ? Convert.ToDecimal(item["MaximumReward"]) : null;
                    rewardConfiguration.IsActive = Convert.ToBoolean(item["IsActive"]);
                    rewardConfiguration.ValidFrom = item["ValidFrom"] != DBNull.Value ? (DateTimeOffset)item["ValidFrom"] : null;
                    rewardConfiguration.ValidTo = item["ValidTo"] != DBNull.Value ? (DateTimeOffset)item["ValidTo"] : null;
                    rewardConfiguration.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    rewardConfiguration.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset)item["CreatedOn"] : null;
                    rewardConfiguration.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    rewardConfiguration.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)item["ModifiedOn"] : null;
                    return rewardConfiguration;
                }
            }
            return rewardConfiguration;
        }

        public async Task<bool> SaveRewardConfigurationAsync(RewardConfiguration rewardConfiguration)
        {
            if (rewardConfiguration != null)
            {

                SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspSaveRewardConfiguration]", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@RewardName", rewardConfiguration.RewardName);
                sqlCommand.Parameters.AddWithValue("@RewardType", rewardConfiguration.RewardType);
                sqlCommand.Parameters.AddWithValue("@RewardValue", rewardConfiguration.RewardValue);
                sqlCommand.Parameters.AddWithValue("@MinimumTransactionAmount", rewardConfiguration.MinimumTransactionAmount);
                sqlCommand.Parameters.AddWithValue("@MaximumReward", rewardConfiguration.MaximumReward);
                sqlCommand.Parameters.AddWithValue("@IsActive", rewardConfiguration.IsActive);
                sqlCommand.Parameters.AddWithValue("@ValidFrom", rewardConfiguration.ValidFrom);
                sqlCommand.Parameters.AddWithValue("@ValidTo", rewardConfiguration.ValidTo);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", rewardConfiguration.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@ModifiedBy", rewardConfiguration.ModifiedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRewardConfigurationAsync(RewardConfiguration rewardConfiguration)
        {
            if (rewardConfiguration != null)
            {


                SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateRewardConfiguration]", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Id", rewardConfiguration.Id);
                sqlCommand.Parameters.AddWithValue("@RewardName", rewardConfiguration.RewardName);
                sqlCommand.Parameters.AddWithValue("@RewardType", rewardConfiguration.RewardType);
                sqlCommand.Parameters.AddWithValue("@RewardValue", rewardConfiguration.RewardValue);
                sqlCommand.Parameters.AddWithValue("@MinimumTransactionAmount", rewardConfiguration.MinimumTransactionAmount);
                sqlCommand.Parameters.AddWithValue("@MaximumReward", (object?)rewardConfiguration.MaximumReward ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@IsActive", rewardConfiguration.IsActive);
                sqlCommand.Parameters.AddWithValue("@ValidFrom", (object?)rewardConfiguration.ValidFrom ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ValidTo", (object?)rewardConfiguration.ValidTo ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ModifiedBy", (object?)rewardConfiguration.ModifiedBy ?? DBNull.Value);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
    
}
