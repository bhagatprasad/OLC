using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class UserWalletManager : IUserWalletManager
    {
        private readonly string connectionString;

        public UserWalletManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<UserWalletLog>> GetAllUsersWalletlogAsync()
        {
            List<UserWalletLog> getUserWalletlogs = new List<UserWalletLog>();

            UserWalletLog log = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllUsersWalletlog]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    log = new UserWalletLog();

                    log.Id = Convert.ToInt64(item["Id"]);
                    log.WalletId = item["WalletId"] != DBNull.Value ? Convert.ToInt64(item["WalletId"]) : null;
                    log.UserId = item["UserId"] != DBNull.Value ? Convert.ToInt64(item["UserId"]) : null;

                    log.Amount = item["Amount"] != DBNull.Value ? Convert.ToDecimal(item["Amount"]) : null;

                    log.TransactionType = item["TransactionType"]?.ToString();
                    log.Description = item["Description"]?.ToString();
                    log.ReferenceId = item["ReferenceId"]?.ToString();
                    log.Currency = item["Currency"]?.ToString();

                    log.IsActive = item["IsActive"] != DBNull.Value ? Convert.ToBoolean(item["IsActive"]) : null;

                    log.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    log.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset)item["CreatedOn"] : null;

                    log.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)item["ModifiedOn"] : null;
                    log.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    getUserWalletlogs.Add(log);
                }
            }

            return getUserWalletlogs;
        }

        public async Task<List<UserWalletLog>> GetAllUserWalletlogByUserIdAsync(long userId)
        {
            List<UserWalletLog> getUserWalletlogs = new List<UserWalletLog>();
            UserWalletLog log = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserWalletLogByUserId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    log = new UserWalletLog();

                    log.Id = Convert.ToInt64(item["Id"]);
                    log.WalletId = item["WalletId"] != DBNull.Value ? Convert.ToInt64(item["WalletId"]) : null;
                    log.UserId = item["UserId"] != DBNull.Value ? Convert.ToInt64(item["UserId"]) : null;
                    log.Amount = item["Amount"] != DBNull.Value ? Convert.ToDecimal(item["Amount"]) : null;
                    log.TransactionType = item["TransactionType"]?.ToString();
                    log.Description = item["Description"]?.ToString();
                    log.ReferenceId = item["ReferenceId"]?.ToString();
                    log.Currency = item["Currency"]?.ToString();
                    log.IsActive = item["IsActive"] != DBNull.Value ? Convert.ToBoolean(item["IsActive"]) : null;
                    log.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    log.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset)item["CreatedOn"] : null;
                    log.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)item["ModifiedOn"] : null;
                    log.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getUserWalletlogs.Add(log);
                }
            }
            return getUserWalletlogs;
        }

        public async Task<List<UserWallet>> GetAllUserWalletsAsync()
        {
            List<UserWallet> getUserWallets = new List<UserWallet>();

            UserWallet wallet = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllWallets]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    wallet = new UserWallet();

                    wallet.Id = Convert.ToInt64(item["Id"]);
                    wallet.UserId = Convert.ToInt64(item["UserId"]);

                    wallet.WalletId = item["WalletId"].ToString();

                    wallet.WalletType = item["WalletType"].ToString();

                    wallet.CurrentBalance = Convert.ToDecimal(item["CurrentBalance"]);

                    wallet.TotalEarned = Convert.ToDecimal(item["TotalEarned"]);

                    wallet.TotalSpent = Convert.ToDecimal(item["TotalSpent"]);

                    wallet.Currency = item["Currency"].ToString();

                    wallet.IsActive = Convert.ToBoolean(item["IsActive"]);


                    wallet.CreatedBy = Convert.ToInt64(item["CreatedBy"]);

                    wallet.CreatedOn = (DateTimeOffset)(item["CreatedOn"]);

                    wallet.ModifiedOn = (DateTimeOffset)(item["ModifiedOn"]);

                    wallet.ModifiedBy = Convert.ToInt64(item["ModifiedBy"]);

                    wallet.UserEmail = item["UserEmail"] != DBNull.Value ? (item["UserEmail"]).ToString() : null;
                    wallet.UserPhone = item["UserPhone"] != DBNull.Value ? (item["UserPhone"]).ToString() : null;

                    wallet.Status = Convert.ToString(item["Status"]);

                    getUserWallets.Add(wallet);
                }
            }

            return getUserWallets;
        }

        public async Task<UserWallet> GetUserWalletByUserIdAsync(long userId)
        {
            UserWallet wallet = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetWalletsByUserId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    wallet = new UserWallet();

                    wallet.Id = Convert.ToInt64(item["Id"]);

                    wallet.UserId = Convert.ToInt64(item["UserId"]);

                    wallet.WalletId = item["WalletId"].ToString();

                    wallet.WalletType = item["WalletType"].ToString();

                    wallet.CurrentBalance = Convert.ToDecimal(item["CurrentBalance"]);

                    wallet.TotalEarned = Convert.ToDecimal(item["TotalEarned"]);

                    wallet.TotalSpent = Convert.ToDecimal(item["TotalSpent"]);

                    wallet.Currency = item["Currency"].ToString();

                    wallet.IsActive = Convert.ToBoolean(item["IsActive"]);

                    wallet.CreatedBy = Convert.ToInt64(item["CreatedBy"]);

                    wallet.CreatedOn = (DateTimeOffset)item["CreatedOn"];

                    wallet.ModifiedOn = (DateTimeOffset)(item["ModifiedOn"]);

                    wallet.ModifiedBy = Convert.ToInt64(item["ModifiedBy"]);

                }
            }
            return wallet;
        }

        public async Task<bool> InsertUserWalletLogAsyn(UserWalletLog userWalletLog)
        {
            if (userWalletLog != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertUserWalletLog]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@WalletId", userWalletLog.WalletId);

                sqlCommand.Parameters.AddWithValue("@RewardAmount", userWalletLog.Amount);

                sqlCommand.Parameters.AddWithValue("@ModifiedBy", userWalletLog.ModifiedBy);

                sqlCommand.Parameters.AddWithValue("@TransactionType", userWalletLog.TransactionType);

                sqlCommand.Parameters.AddWithValue("@Description", userWalletLog.Description);

                sqlCommand.Parameters.AddWithValue("@ReferenceId", userWalletLog.ReferenceId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> SaveUserWalletAsync(UserWallet userWallet)
        {
            if (userWallet != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspSaveUserWallet]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@userId", userWallet.UserId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateUserWalletBalanceAsync(UserWalletLog userWalletLog)
        {
            if (userWalletLog != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateUserWalletBalance]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@WalletId", userWalletLog.WalletId);

                sqlCommand.Parameters.AddWithValue("@RewardAmount", userWalletLog.Amount);

                sqlCommand.Parameters.AddWithValue("@ModifiedBy", userWalletLog.ModifiedBy);

                sqlCommand.Parameters.AddWithValue("@TransactionType", userWalletLog.TransactionType);

                sqlCommand.Parameters.AddWithValue("@Description", userWalletLog.Description);

                sqlCommand.Parameters.AddWithValue("@ReferenceId", userWalletLog.ReferenceId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public Task<bool> UpdateUserWalletBalanceAsync(UserWallet userWallet)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserWalletDetails>> GetAllExecutiveUserWalletDetailsAsync()
        {
            List<UserWalletDetails> userWalletDetailsList = new List<UserWalletDetails>();
            
                UserWalletDetails userWalletDetails = null;

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllExecutiveUserWalletDetails]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();

                da.Fill(dt);
                sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        userWalletDetails = new UserWalletDetails();

                        userWalletDetails.Id = Convert.ToInt64(row["Id"]);
                        userWalletDetails.UserId = Convert.ToInt64(row["UserId"]);
                        userWalletDetails.WalletId = row["WalletId"].ToString();
                        userWalletDetails.WalletType = row["Wallettype"].ToString();
                        userWalletDetails.CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]);
                        userWalletDetails.TotalEarned = Convert.ToDecimal(row["TotalEarned"]);
                        userWalletDetails.TotalSpent = Convert.ToDecimal(row["TotalSpent"]);
                        userWalletDetails.Currency = Convert.ToString(row["Currency"]);
                        userWalletDetails.UserEmail = row["UserEmail"] != DBNull.Value ? (row["UserEmail"]).ToString() : null;
                        userWalletDetails.UserPhone = row["UserPhone"] != DBNull.Value ? (row["UserPhone"]).ToString() : null;
                        userWalletDetails.Status = row["Status"] != DBNull.Value ? (row["Status"]).ToString() : null;
                        userWalletDetails.IsActive = Convert.ToBoolean(row["Isactive"]);
                        userWalletDetails.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;
                        userWalletDetails.CreatedOn = (DateTimeOffset)(row["CreatedOn"]);
                        userWalletDetails.ModifiedOn = (DateTimeOffset)(row["ModifiedOn"]);
                        userWalletDetails.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;
                        userWalletDetailsList.Add(userWalletDetails);
                    }
                }
                return userWalletDetailsList;
        }

    }
}
        
        

 
