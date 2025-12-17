using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class TransactionRewardManager : ITransactionRewardManager
    {
        private readonly string connectionString;
        public TransactionRewardManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<TransactionRewardDetails>> GetAllExecutiveTransactionRewardDetailsAsync()
        {
            List<TransactionRewardDetails> rewardDetailsList = new List<TransactionRewardDetails>();
            TransactionRewardDetails rewardDetails = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllExecutiveTransactionRewardDetails]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);



            sqlConnection.Close();
            if(dt.Rows.Count>0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    rewardDetails = new TransactionRewardDetails();

                    rewardDetails.UserId = dr["UserId"] != DBNull.Value ? dr["UserId"].ToString() : null;
                    rewardDetails.WalletId = dr["WalletId"].ToString();
                    rewardDetails.PaymentOrderReferenceId = dr["PaymentOrderReferenceId"].ToString();

                    rewardDetails.TotalEarned = dr["TotalEarned"] != DBNull.Value ? Convert.ToDecimal(dr["TotalEarned"]) : null;
                    rewardDetails.TotalSpent = dr["TotalSpent"] != DBNull.Value ? Convert.ToDecimal(dr["TotalSpent"]) : null;
                    rewardDetails.CurrentBalance = dr["CurrentBalance"] != DBNull.Value ? Convert.ToDecimal(dr["CurrentBalance"]) : null;

                    rewardDetails.ChargeableAmount = Convert.ToDecimal(dr["ChargeableAmount"]);
                    rewardDetails.DepositableAmount = Convert.ToDecimal(dr["DepositableAmount"]);

                    rewardDetails.RewardAmount = dr["RewardAmount"] != DBNull.Value ? Convert.ToDecimal(dr["RewardAmount"]) : null;

                    rewardDetails.AccountHolderName = dr["AccountHolderName"] != DBNull.Value ? dr["AccountHolderName"].ToString() : null;

                    rewardDetails.CardNumber = dr["CardNumber"] != DBNull.Value ? dr["CardNumber"].ToString() : null;
                    rewardDetails.AccountNumber = dr["AccountNumber"] != DBNull.Value ? dr["AccountNumber"].ToString() : null;

                    rewardDetails.CreatedBy = dr["CreatedBy"] != DBNull.Value ? dr["CreatedBy"].ToString() : null;
                    rewardDetails.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)dr["CreatedOn"] : null;

                    rewardDetails.IsActive = dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : null;

                    rewardDetailsList.Add(rewardDetails);

                }
            }
            return rewardDetailsList;
        }

        public async Task<List<TransactionReward>> GetAllTransactionRewardsAsync()
        {
            List<TransactionReward> rewards = new List<TransactionReward>();

            TransactionReward reward = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[GetAllTransactionRewards]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    reward = new TransactionReward();

                    reward.Id = Convert.ToInt64(item["Id"]);

                    reward.PaymentOrderId = Convert.ToInt64(item["PaymentOrderId"]);

                    reward.UserId = Convert.ToInt64(item["UserId"]);

                    reward.RewardConfigurationId = Convert.ToInt64(item["RewardConfigurationId"]);

                    reward.TransactionAmount = item["TransactionAmount"] != DBNull.Value ? Convert.ToDecimal(item["TransactionAmount"]) : 0;
     
                    reward.RewardAmount = item["RewardAmount"] != DBNull.Value ? Convert.ToDecimal(item["RewardAmount"]) : 0;
                     
                    reward.RewardRate = item["RewardRate"] != DBNull.Value ? Convert.ToDecimal(item["RewardRate"]) : 0;
                     
                    reward.RewardStatus = item["RewardStatus"]?.ToString();

                    reward.CreditedToWalletId = item["CreditedToWalletId"] != DBNull.Value ? Convert.ToInt64(item["CreditedToWalletId"]) : (long?)null;
                    
                    reward.CreditedOn = item["CreditedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreditedOn"] : null;
                   
                    reward.ExpiryDate = item["ExpiryDate"] != DBNull.Value ? (DateTimeOffset?)item["ExpiryDate"] : null;
                        
                    reward.IsActive = item["IsActive"] != DBNull.Value ? Convert.ToBoolean(item["IsActive"]) : true;
                     
                    reward.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : (long?)null;

                    reward.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset)item["CreatedOn"] : DateTimeOffset.UtcNow;
                       
                    reward.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)item["ModifiedOn"] : DateTimeOffset.UtcNow;
                    
                    reward.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : (long?)null;
                       
                    rewards.Add(reward);
                }

            }
            return rewards;
        }

        public async Task<List<TransactionReward>> GetAllTransactionRewardsByUserIdAsync(long userId)
        {
            List<TransactionReward> rewards = new List<TransactionReward>();

            TransactionReward reward = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetTransactionRewardsByUserId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@UserId",userId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    reward = new TransactionReward();

                    reward.Id = Convert.ToInt64(item["Id"]);

                    reward.PaymentOrderId = Convert.ToInt64(item["PaymentOrderId"]);

                    reward.UserId = Convert.ToInt64(item["UserId"]);

                    reward.RewardConfigurationId = Convert.ToInt64(item["RewardConfigurationId"]);

                    reward.TransactionAmount = item["TransactionAmount"] != DBNull.Value ? Convert.ToDecimal(item["TransactionAmount"]) : 0;

                    reward.RewardAmount = item["RewardAmount"] != DBNull.Value ? Convert.ToDecimal(item["RewardAmount"]) : 0;

                    reward.RewardRate = item["RewardRate"] != DBNull.Value ? Convert.ToDecimal(item["RewardRate"]) : 0;

                    reward.RewardStatus = item["RewardStatus"]?.ToString();

                    reward.CreditedToWalletId = item["CreditedToWalletId"] != DBNull.Value ? Convert.ToInt64(item["CreditedToWalletId"]) : (long?)null;

                    reward.CreditedOn = item["CreditedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreditedOn"] : null;

                    reward.ExpiryDate = item["ExpiryDate"] != DBNull.Value ? (DateTimeOffset?)item["ExpiryDate"] : null;

                    reward.IsActive = item["IsActive"] != DBNull.Value ? Convert.ToBoolean(item["IsActive"]) : true;

                    reward.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : (long?)null;

                    reward.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset)item["CreatedOn"] : DateTimeOffset.UtcNow;

                    reward.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)item["ModifiedOn"] : DateTimeOffset.UtcNow;

                    reward.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : (long?)null;

                    rewards.Add(reward);
                }

            }
            return rewards;
        }

        public async Task<TransactionReward> GetTransactionRewardByPaymentOrderIdAsync(long paymentOrderId)
        {
            TransactionReward reward = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[GetTransactionRewardByPaymentOrderId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@PaymentOrderId", paymentOrderId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    reward = new TransactionReward();

                    reward.Id = Convert.ToInt64(item["Id"]);

                    reward.PaymentOrderId = Convert.ToInt64(item["PaymentOrderId"]);

                    reward.UserId = Convert.ToInt64(item["UserId"]);

                    reward.RewardConfigurationId = Convert.ToInt64(item["RewardConfigurationId"]);

                    reward.TransactionAmount = item["TransactionAmount"] != DBNull.Value ? Convert.ToDecimal(item["TransactionAmount"]) : 0;

                    reward.RewardAmount = item["RewardAmount"] != DBNull.Value ? Convert.ToDecimal(item["RewardAmount"]) : 0;

                    reward.RewardRate = item["RewardRate"] != DBNull.Value ? Convert.ToDecimal(item["RewardRate"]) : 0;

                    reward.RewardStatus = item["RewardStatus"]?.ToString();

                    reward.CreditedToWalletId = item["CreditedToWalletId"] != DBNull.Value ? Convert.ToInt64(item["CreditedToWalletId"]) : (long?)null;

                    reward.CreditedOn = item["CreditedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreditedOn"] : null;

                    reward.ExpiryDate = item["ExpiryDate"] != DBNull.Value ? (DateTimeOffset?)item["ExpiryDate"] : null;

                    reward.IsActive = item["IsActive"] != DBNull.Value ? Convert.ToBoolean(item["IsActive"]) : true;

                    reward.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : (long?)null;

                    reward.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset)item["CreatedOn"] : DateTimeOffset.UtcNow;

                    reward.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)item["ModifiedOn"] : DateTimeOffset.UtcNow;

                    reward.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : (long?)null;
                }
            }
            return reward;
        }

        public async Task<bool> InsertTransactionRewardAsync(TransactionReward transactionReward)
        {
            if (transactionReward != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[[dbo].[uspInsertTransactionReward]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@PaymentOrderId", transactionReward.PaymentOrderId);

                sqlCommand.Parameters.AddWithValue("@UserId", transactionReward.UserId);

                sqlCommand.Parameters.AddWithValue("@TransactionAmount", transactionReward.TransactionAmount);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }
    }
}
