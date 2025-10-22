using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class AccountTypeManager : IAccountTypeManager
    {
        private readonly string connectionString;
        public AccountTypeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<AccountType> GetAccountTypeByIdAsync(long accountTypeId)
        {
            AccountType getAccountTypeById = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAccountTypeById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@accountTypeId", accountTypeId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getAccountTypeById = new AccountType();

                    getAccountTypeById.Id = Convert.ToInt64(item["Id"]);

                    getAccountTypeById.Name = (item["Name"].ToString());

                    getAccountTypeById.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getAccountTypeById.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getAccountTypeById.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getAccountTypeById.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getAccountTypeById.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return getAccountTypeById;
        }
        public async Task<List<AccountType>> GetAccountTypeAsync()
        {
            List<AccountType> getAccountTypes = new List<AccountType>();

            AccountType getAccountType = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAccountTypes]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getAccountType = new AccountType();

                    getAccountType.Id = Convert.ToInt64(item["Id"]);

                    getAccountType.Name = item["Name"].ToString();

                    getAccountType.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getAccountType.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getAccountType.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getAccountType.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getAccountType.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getAccountTypes.Add(getAccountType);
                }
            }

            return getAccountTypes;
        }
        public async Task<bool> InsertUserAccountTypeAsync(AccountType accountType)
        {
            if (accountType != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertAccountType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@name", accountType.Name);

                sqlCommand.Parameters.AddWithValue("@code", accountType.Code);

                sqlCommand.Parameters.AddWithValue("@createdBy", accountType.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }
        public async Task<bool> UpdateUserAccountTypeAsync(AccountType accountType)
        {

            if (accountType != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateAccountType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", accountType.Id);

                sqlCommand.Parameters.AddWithValue("@name", accountType.Name);

                sqlCommand.Parameters.AddWithValue("@code", accountType.Code);

                sqlCommand.Parameters.AddWithValue("@createdBy", accountType.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUserAccoutntTypeAsync(long accountTypeId)
        {
            if (accountTypeId != 0)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteAccountType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@accountTypeId", accountTypeId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }
}