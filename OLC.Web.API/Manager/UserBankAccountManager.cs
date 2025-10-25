using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{

    public class UserBankAccountManager : IUserBankAccountManager
    {
        private readonly string connectionString;

        public UserBankAccountManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<UserBankAccount> GetUserBankAccountByIdAsync(long id)
        {
            UserBankAccount userBankAccount = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserBankAccountById]", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                connection.Close();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    userBankAccount = new UserBankAccount
                    {
                        Id = Convert.ToInt64(row["Id"]),
                        UserId = Convert.ToInt64(row["UserId"]),
                        AccountHolderName = row["AccountHolderName"] != DBNull.Value ? row["AccountHolderName"].ToString() : null,
                        BankName = row["BankName"] != DBNull.Value ? row["BankName"].ToString() : null,
                        BranchName = row["BranchName"] != DBNull.Value ? row["BranchName"].ToString() : null,
                        AccountNumber = row["AccountNumber"] != DBNull.Value ? row["AccountNumber"].ToString() : null,
                        LastFourDigits = row["LastFourDigits"] != DBNull.Value ? row["LastFourDigits"].ToString() : null,
                        AccountType = row["AccountType"] != DBNull.Value ? row["AccountType"].ToString() : null,
                        RoutingNumber = row["RoutingNumber"] != DBNull.Value ? row["RoutingNumber"].ToString() : null,
                        IFSCCode = row["IFSCCode"] != DBNull.Value ? row["IFSCCode"].ToString() : null,
                        SWIFTCode = row["SWIFTCode"] != DBNull.Value ? row["SWIFTCode"].ToString() : null,
                        Currency = row["Currency"] != DBNull.Value ? row["Currency"].ToString() : null,
                        IsPrimary = row["IsPrimary"] != DBNull.Value ? (bool?)row["IsPrimary"] : null,
                        IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null,
                        VerifiedOn = row["VerifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["VerifiedOn"] : null,
                        VerificationStatus = row["VerificationStatus"] != DBNull.Value ? row["VerificationStatus"].ToString() : null,
                        VerificationAttempts = row["VerificationAttempts"] != DBNull.Value ? (int?)row["VerificationAttempts"] : null,
                        LastVerificationAttempt = row["LastVerificationAttempt"] != DBNull.Value ? (DateTimeOffset?)row["LastVerificationAttempt"] : null,
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? (long?)row["CreatedBy"] : null,
                        CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null,
                        ModifiedBy = row["ModifiedBy"] != DBNull.Value ? (long?)row["ModifiedBy"] : null,
                        ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null
                    };
                }
            }
            return userBankAccount;
        }

        public async Task<List<UserBankAccount>> GetAllUserBankAccountByUserIdAsync(long userId)
        {
            List<UserBankAccount> userBankAccount = new List<UserBankAccount>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllUserBankAccountByUserId]", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@userId", userId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                connection.Close();
                foreach (DataRow row in dt.Rows)
                {
                    UserBankAccount account = new UserBankAccount
                    {

                        Id = Convert.ToInt64(row["Id"]),
                        UserId = Convert.ToInt64(row["UserId"]),
                        AccountHolderName = row["AccountHolderName"] != DBNull.Value ? row["AccountHolderName"].ToString() : null,
                        BankName = row["BankName"] != DBNull.Value ? row["BankName"].ToString() : null,
                        BranchName = row["BranchName"] != DBNull.Value ? row["BranchName"].ToString() : null,
                        AccountNumber = row["AccountNumber"] != DBNull.Value ? row["AccountNumber"].ToString() : null,
                        LastFourDigits = row["LastFourDigits"] != DBNull.Value ? row["LastFourDigits"].ToString() : null,
                        AccountType = row["AccountType"] != DBNull.Value ? row["AccountType"].ToString() : null,
                        RoutingNumber = row["RoutingNumber"] != DBNull.Value ? row["RoutingNumber"].ToString() : null,
                        IFSCCode = row["IFSCCode"] != DBNull.Value ? row["IFSCCode"].ToString() : null,
                        SWIFTCode = row["SWIFTCode"] != DBNull.Value ? row["SWIFTCode"].ToString() : null,
                        Currency = row["Currency"] != DBNull.Value ? row["Currency"].ToString() : null,
                        IsPrimary = row["IsPrimary"] != DBNull.Value ? (bool?)row["IsPrimary"] : null,
                        IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null,
                        VerifiedOn = row["VerifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["VerifiedOn"] : null,
                        VerificationStatus = row["VerificationStatus"] != DBNull.Value ? row["VerificationStatus"].ToString() : null,
                        VerificationAttempts = row["VerificationAttempts"] != DBNull.Value ? (int?)row["VerificationAttempts"] : null,
                        LastVerificationAttempt = row["LastVerificationAttempt"] != DBNull.Value ? (DateTimeOffset?)row["LastVerificationAttempt"] : null,
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? (long?)row["CreatedBy"] : null,
                        CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null,
                        ModifiedBy = row["ModifiedBy"] != DBNull.Value ? (long?)row["ModifiedBy"] : null,
                        ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null
                    };
                    userBankAccount.Add(account);
                }
            }
            return userBankAccount;
        }

        public async Task<List<UserBankAccount>> GetAllUserBankAccountsAsync()
        {
            List<UserBankAccount> userBankAccounts = new List<UserBankAccount>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllUserBankAccounts]", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                connection.Close();
                foreach (DataRow row in dt.Rows)
                {
                    UserBankAccount account = new UserBankAccount
                    {
                        Id = Convert.ToInt64(row["Id"]),
                        UserId = Convert.ToInt64(row["UserId"]),
                        AccountHolderName = row["AccountHolderName"] != DBNull.Value ? row["AccountHolderName"].ToString() : null,
                        BankName = row["BankName"] != DBNull.Value ? row["BankName"].ToString() : null,
                        BranchName = row["BranchName"] != DBNull.Value ? row["BranchName"].ToString() : null,
                        AccountNumber = row["AccountNumber"] != DBNull.Value ? row["AccountNumber"].ToString() : null,
                        LastFourDigits = row["LastFourDigits"] != DBNull.Value ? row["LastFourDigits"].ToString() : null,
                        AccountType = row["AccountType"] != DBNull.Value ? row["AccountType"].ToString() : null,
                        RoutingNumber = row["RoutingNumber"] != DBNull.Value ? row["RoutingNumber"].ToString() : null,
                        IFSCCode = row["IFSCCode"] != DBNull.Value ? row["IFSCCode"].ToString() : null,
                        SWIFTCode = row["SWIFTCode"] != DBNull.Value ? row["SWIFTCode"].ToString() : null,
                        Currency = row["Currency"] != DBNull.Value ? row["Currency"].ToString() : null,
                        IsPrimary = row["IsPrimary"] != DBNull.Value ? (bool?)row["IsPrimary"] : null,
                        IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null,
                        VerifiedOn = row["VerifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["VerifiedOn"] : null,
                        VerificationStatus = row["VerificationStatus"] != DBNull.Value ? row["VerificationStatus"].ToString() : null,
                        VerificationAttempts = row["VerificationAttempts"] != DBNull.Value ? (int?)row["VerificationAttempts"] : null,
                        LastVerificationAttempt = row["LastVerificationAttempt"] != DBNull.Value ? (DateTimeOffset?)row["LastVerificationAttempt"] : null,
                        CreatedBy = row["CreatedBy"] != DBNull.Value ? (long?)row["CreatedBy"] : null,
                        CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null,
                        ModifiedBy = row["ModifiedBy"] != DBNull.Value ? (long?)row["ModifiedBy"] : null,
                        ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null
                    };
                    userBankAccounts.Add(account);
                }
            }
            return userBankAccounts;
        }


        public async Task<bool> InsertUserBankAccountAsync(UserBankAccount userBankAccount)
        {
            if (userBankAccount != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertUserBankAccount]", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userBankAccount.UserId);
                    sqlCommand.Parameters.AddWithValue("@AccountHolderName", userBankAccount.AccountHolderName);
                    sqlCommand.Parameters.AddWithValue("@BankName", userBankAccount.BankName);
                    sqlCommand.Parameters.AddWithValue("@BranchName", userBankAccount.BranchName ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@AccountNumber", userBankAccount.AccountNumber ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@LastFourDigits", userBankAccount.LastFourDigits);
                    sqlCommand.Parameters.AddWithValue("@AccountType", userBankAccount.AccountType);
                    sqlCommand.Parameters.AddWithValue("@RoutingNumber", userBankAccount.RoutingNumber ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@IFSCCode", userBankAccount.IFSCCode ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@SWIFTCode", userBankAccount.SWIFTCode ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@Currency", userBankAccount.Currency ?? "USD");
                    sqlCommand.Parameters.AddWithValue("@IsPrimary", userBankAccount.IsPrimary ?? false);
                    sqlCommand.Parameters.AddWithValue("@IsActive", userBankAccount.IsActive ?? true);
                    sqlCommand.Parameters.AddWithValue("@VerificationStatus", userBankAccount.VerificationStatus ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@VerificationAttempts", userBankAccount.VerificationAttempts ?? 0);

                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserBankAccountAsync(UserBankAccount userBankAccount)
        {
            if (userBankAccount != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateUserBankAccount]", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", userBankAccount.Id);
                    sqlCommand.Parameters.AddWithValue("@UserId", userBankAccount.UserId);
                    sqlCommand.Parameters.AddWithValue("@AccountHolderName", userBankAccount.AccountHolderName);
                    sqlCommand.Parameters.AddWithValue("@BankName", userBankAccount.BankName);
                    sqlCommand.Parameters.AddWithValue("@BranchName", userBankAccount.BranchName ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@AccountNumber", userBankAccount.AccountNumber ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@LastFourDigits", userBankAccount.LastFourDigits);
                    sqlCommand.Parameters.AddWithValue("@AccountType", userBankAccount.AccountType);
                    sqlCommand.Parameters.AddWithValue("@RoutingNumber", userBankAccount.RoutingNumber ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@IFSCCode", userBankAccount.IFSCCode ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@SWIFTCode", userBankAccount.SWIFTCode ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@Currency", userBankAccount.Currency ?? "USD");
                    sqlCommand.Parameters.AddWithValue("@IsPrimary", userBankAccount.IsPrimary ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@IsActive", userBankAccount.IsActive ?? true);
                    sqlCommand.Parameters.AddWithValue("@VerifiedOn", userBankAccount.VerifiedOn ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@VerificationStatus", userBankAccount.VerificationStatus ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@VerificationAttempts", userBankAccount.VerificationAttempts ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@LastVerificationAttempt", userBankAccount.LastVerificationAttempt ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@ModifiedBy", userBankAccount.ModifiedBy);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserBankAccountAsync(long id)
        {
            if (id != 0)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteUserBankAccount]", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            return false;
        }
    }

}

        