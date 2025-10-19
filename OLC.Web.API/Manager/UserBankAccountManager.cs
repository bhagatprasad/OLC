using OLC.Web.API.Models;  // Assuming this is where UserBankAccount is defined
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

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

                    sqlCommand.Parameters.AddWithValue("@userId", userBankAccount.UserId);
                    sqlCommand.Parameters.AddWithValue("@accountHolderName", userBankAccount.AccountHolderName);
                    sqlCommand.Parameters.AddWithValue("@BankName", userBankAccount.BankName);
                    sqlCommand.Parameters.AddWithValue("@BranchName", userBankAccount.BranchName);
                    sqlCommand.Parameters.AddWithValue("@AccountNumber", userBankAccount.AccountNumber);
                    sqlCommand.Parameters.AddWithValue("@LastFourDigits", userBankAccount.LastFourDigits);
                    sqlCommand.Parameters.AddWithValue("@AccountType", userBankAccount.AccountType);
                    sqlCommand.Parameters.AddWithValue("@RoutingNumber", userBankAccount.RoutingNumber);
                    sqlCommand.Parameters.AddWithValue("@IFSCCode", userBankAccount.IFSCCode);
                    sqlCommand.Parameters.AddWithValue("@SWIFTCode", userBankAccount.SWIFTCode);
                    sqlCommand.Parameters.AddWithValue("@Currency", userBankAccount.Currency);
                    sqlCommand.Parameters.AddWithValue("@IsPrimary", userBankAccount.IsPrimary);
                    sqlCommand.Parameters.AddWithValue("@IsActive", userBankAccount.IsActive);
                    sqlCommand.Parameters.AddWithValue("@VerifiedOn", userBankAccount.VerifiedOn);
                    sqlCommand.Parameters.AddWithValue("@VerificationStatus", userBankAccount.VerificationStatus);
                    sqlCommand.Parameters.AddWithValue("@CreatedBy", userBankAccount.CreatedBy);
                    sqlCommand.Parameters.AddWithValue("@CreatedOn", userBankAccount.CreatedOn);
                    sqlCommand.Parameters.AddWithValue("@ModifiedBy", userBankAccount.ModifiedBy);
                    sqlCommand.Parameters.AddWithValue("@ModifiedOn", userBankAccount.ModifiedOn);

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
                    sqlCommand.Parameters.AddWithValue("@id", userBankAccount.Id);
                    sqlCommand.Parameters.AddWithValue("@userId", userBankAccount.UserId);
                    sqlCommand.Parameters.AddWithValue("@accountHolderName", userBankAccount.AccountHolderName);
                    sqlCommand.Parameters.AddWithValue("@BankName", userBankAccount.BankName);
                    sqlCommand.Parameters.AddWithValue("@BranchName", userBankAccount.BranchName);
                    sqlCommand.Parameters.AddWithValue("@AccountNumber", userBankAccount.AccountNumber);
                    sqlCommand.Parameters.AddWithValue("@LastFourDigits", userBankAccount.LastFourDigits);
                    sqlCommand.Parameters.AddWithValue("@AccountType", userBankAccount.AccountType);
                    sqlCommand.Parameters.AddWithValue("@RoutingNumber", userBankAccount.RoutingNumber);
                    sqlCommand.Parameters.AddWithValue("@IFSCCode", userBankAccount.IFSCCode);
                    sqlCommand.Parameters.AddWithValue("@SWIFTCode", userBankAccount.SWIFTCode);
                    sqlCommand.Parameters.AddWithValue("@Currency", userBankAccount.Currency);
                    sqlCommand.Parameters.AddWithValue("@IsPrimary", userBankAccount.IsPrimary);
                    sqlCommand.Parameters.AddWithValue("@IsActive", userBankAccount.IsActive);
                    sqlCommand.Parameters.AddWithValue("@VerifiedOn", userBankAccount.VerifiedOn);
                    sqlCommand.Parameters.AddWithValue("@VerificationStatus", userBankAccount.VerificationStatus);
                    sqlCommand.Parameters.AddWithValue("@CreatedBy", userBankAccount.CreatedBy);
                    sqlCommand.Parameters.AddWithValue("@CreatedOn", userBankAccount.CreatedOn);
                    sqlCommand.Parameters.AddWithValue("@ModifiedBy", userBankAccount.ModifiedBy);
                    sqlCommand.Parameters.AddWithValue("@ModifiedOn", userBankAccount.ModifiedOn);
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

        