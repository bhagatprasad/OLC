using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace OLC.Web.API.Manager
{
    public class UserWalletDetailsManager : IUserWalletDetailsManager
    {
        private readonly string connectionString;
        public UserWalletDetailsManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<UserWalletDetails> uspGetUserWalletDetailsByUserIdAsync(long userId)
        {
            UserWalletDetails userWalletDetails = new UserWalletDetails();

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserWalletDetailsByUserId]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserId", userId);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    userWalletDetails = new UserWalletDetails();

                    userWalletDetails.Id = Convert.ToInt64(item["Id"]);
                    userWalletDetails.UserId = Convert.ToInt64(item["UserId"]);
                    userWalletDetails.WalletId = item["WalletId"].ToString();
                    userWalletDetails.WalletType = item["Wallettype"].ToString();
                    userWalletDetails.CurrentBalance = Convert.ToDecimal(item["CurrentBalance"]);
                    userWalletDetails.TotalEarned = Convert.ToDecimal(item["TotalEarned"]);
                    userWalletDetails.TotalSpent = Convert.ToDecimal(item["TotalSpent"]);
                    userWalletDetails.Currency = Convert.ToString(item["Currency"]);
                    userWalletDetails.UserEmail = item["UserEmail"] != DBNull.Value ? (item["UserEmail"]).ToString():null;
                    userWalletDetails.UserPhone = item["UserPhone"] != DBNull.Value ? (item["UserPhone"]).ToString() : null;
                    userWalletDetails.IsActive = Convert.ToBoolean(item["Isactive"]);
                    userWalletDetails.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    userWalletDetails.CreatedOn = (DateTimeOffset)(item["CreatedOn"]);
                    userWalletDetails.ModifiedOn = (DateTimeOffset)(item["ModifiedOn"]);
                    userWalletDetails.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                }
            }
            return userWalletDetails;

        }
    }
}
