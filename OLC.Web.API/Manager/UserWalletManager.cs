using Microsoft.Extensions.Logging.Console;
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

                    wallet.Currency = item["Currency"].ToString() ;

                    wallet.IsActive = Convert.ToBoolean(item["IsActive"]) ;

                    wallet.CreatedBy = Convert.ToInt64(item["CreatedBy"]) ;

                    wallet.CreatedOn = (DateTimeOffset)(item["CreatedOn"]);

                    wallet.ModifiedOn = (DateTimeOffset)(item["ModifiedOn"]);

                    wallet.ModifiedBy = Convert.ToInt64(item["ModifiedBy"]);
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

                    wallet.Id = Convert.ToInt64(item["Id"]) ;

                    wallet.UserId = Convert.ToInt64(item["UserId"]) ;

                    wallet.WalletId = item["WalletId"].ToString() ;

                    wallet.WalletType = item["WalletType"].ToString() ;

                    wallet.CurrentBalance = Convert.ToDecimal(item["CurrentBalance"]) ;

                    wallet.TotalEarned = Convert.ToDecimal(item["TotalEarned"]);

                    wallet.TotalSpent = Convert.ToDecimal(item["TotalSpent"]);

                    wallet.Currency = item["Currency"].ToString();

                    wallet.IsActive = Convert.ToBoolean(item["IsActive"]);

                    wallet.CreatedBy = Convert.ToInt64(item["CreatedBy"]);

                    wallet.CreatedOn = (DateTimeOffset)item["CreatedOn"] ;

                    wallet.ModifiedOn = (DateTimeOffset)(item["ModifiedOn"]);

                    wallet.ModifiedBy = Convert.ToInt64(item["ModifiedBy"]);

                }
            }
            return wallet;
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

        public async Task<bool> UpdateUserWalletBalanceAsync(UserWallet userWallet)
        {
            if (userWallet != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateUSerWalletBalance]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@userId", userWallet.UserId);

                sqlCommand.Parameters.AddWithValue("@currentBalance", userWallet.CurrentBalance);

                sqlCommand.Parameters.AddWithValue("@totalEarned", userWallet.TotalEarned);

                sqlCommand.Parameters.AddWithValue("@totalSpent", userWallet.TotalSpent);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }
    }
}
