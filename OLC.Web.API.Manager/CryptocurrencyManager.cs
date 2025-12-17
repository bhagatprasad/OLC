using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class CryptocurrencyManager:ICryptocurrencyManager
    {
        private readonly string connectionString;
        public CryptocurrencyManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Cryptocurrency>> GetAllCryptocurrenciesAsync()
        {
            List<Cryptocurrency> getCryptocurrencies = new List<Cryptocurrency>();

            Cryptocurrency getCryptocurrency = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllCryptocurrencies]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getCryptocurrency = new Cryptocurrency();

                    getCryptocurrency.Id = Convert.ToInt64(item["Id"]);
                    getCryptocurrency.Symbol = item["Symbol"].ToString();
                    getCryptocurrency.Name = item["Name"].ToString();
                    getCryptocurrency.Blockchain = item["Blockchain"].ToString();
                    getCryptocurrency.ContractAddress = item["ContractAddress"] != DBNull.Value ? item["ContractAddress"].ToString() : null;
                    getCryptocurrency.Decimals = Convert.ToInt32(item["Decimals"]);
                    getCryptocurrency.IsStablecoin = item["IsStablecoin"] != DBNull.Value ? Convert.ToBoolean(item["IsStablecoin"]) : false;
                    getCryptocurrency.MinDepositAmount = item["MinDepositAmount"] != DBNull.Value ? Convert.ToDecimal(item["MinDepositAmount"]) : 0;
                    getCryptocurrency.MinWithdrawalAmount = item["MinWithdrawalAmount"] != DBNull.Value ? Convert.ToDecimal(item["MinWithdrawalAmount"]) : 0;
                    getCryptocurrency.WithdrawalFee = item["WithdrawalFee"] != DBNull.Value ? Convert.ToDecimal(item["WithdrawalFee"]) : 0;
                    getCryptocurrency.IconUrl = item["IconUrl"] != DBNull.Value ? item["IconUrl"].ToString() : null;
                    getCryptocurrency.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : (long?)null;
                    getCryptocurrency.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getCryptocurrency.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : (long?)null;
                    getCryptocurrency.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getCryptocurrency.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getCryptocurrencies.Add(getCryptocurrency);
                }
            }

            return getCryptocurrencies;
        }

        public async Task<Cryptocurrency> GetCryptocurrencyByIdAsync(long id)
        {
            Cryptocurrency getCryptocurrency = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCryptocurrencyById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getCryptocurrency = new Cryptocurrency();

                    getCryptocurrency.Id = Convert.ToInt64(item["Id"]);
                    getCryptocurrency.Symbol = item["Symbol"].ToString();
                    getCryptocurrency.Name = item["Name"].ToString();
                    getCryptocurrency.Blockchain = item["Blockchain"].ToString();
                    getCryptocurrency.ContractAddress = item["ContractAddress"] != DBNull.Value ? item["ContractAddress"].ToString() : null;
                    getCryptocurrency.Decimals = Convert.ToInt32(item["Decimals"]);
                    getCryptocurrency.IsStablecoin = item["IsStablecoin"] != DBNull.Value ? Convert.ToBoolean(item["IsStablecoin"]) : false;
                    getCryptocurrency.MinDepositAmount = item["MinDepositAmount"] != DBNull.Value ? Convert.ToDecimal(item["MinDepositAmount"]) : 0;
                    getCryptocurrency.MinWithdrawalAmount = item["MinWithdrawalAmount"] != DBNull.Value ? Convert.ToDecimal(item["MinWithdrawalAmount"]) : 0;
                    getCryptocurrency.WithdrawalFee = item["WithdrawalFee"] != DBNull.Value ? Convert.ToDecimal(item["WithdrawalFee"]) : 0;
                    getCryptocurrency.IconUrl = item["IconUrl"] != DBNull.Value ? item["IconUrl"].ToString() : null;
                    getCryptocurrency.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : (long?)null;
                    getCryptocurrency.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getCryptocurrency.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : (long?)null;
                    getCryptocurrency.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getCryptocurrency.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return getCryptocurrency;
        }

        public async Task<bool> InserCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            if (cryptocurrency != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspInsertCryptocurrency]", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Symbol", cryptocurrency.Symbol);
                cmd.Parameters.AddWithValue("@Name", cryptocurrency.Name);
                cmd.Parameters.AddWithValue("@Blockchain", cryptocurrency.Blockchain);
                cmd.Parameters.AddWithValue("@ContractAddress", cryptocurrency.ContractAddress);
                cmd.Parameters.AddWithValue("@Decimals", cryptocurrency.Decimals);
                cmd.Parameters.AddWithValue("@IsStablecoin", cryptocurrency.IsStablecoin);
                cmd.Parameters.AddWithValue("@MinDepositAmount",cryptocurrency.MinDepositAmount);
                cmd.Parameters.AddWithValue("@MinWithdrawalAmount", cryptocurrency.MinWithdrawalAmount);
                cmd.Parameters.AddWithValue("@WithdrawalFee", cryptocurrency.WithdrawalFee);
                cmd.Parameters.AddWithValue("@IconUrl", cryptocurrency.IconUrl);
                cmd.Parameters.AddWithValue("@CreatedBy", cryptocurrency.CreatedBy);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            if (cryptocurrency != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspUpdateCryptocurrency]", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", cryptocurrency.Id);
                cmd.Parameters.AddWithValue("@Symbol", cryptocurrency.Symbol);
                cmd.Parameters.AddWithValue("@Name", cryptocurrency.Name);
                cmd.Parameters.AddWithValue("@Blockchain", cryptocurrency.Blockchain);
                cmd.Parameters.AddWithValue("@ContractAddress", cryptocurrency.ContractAddress);
                cmd.Parameters.AddWithValue("@Decimals", cryptocurrency.Decimals);
                cmd.Parameters.AddWithValue("@IsStablecoin", cryptocurrency.IsStablecoin);
                cmd.Parameters.AddWithValue("@MinDepositAmount", cryptocurrency.MinDepositAmount);
                cmd.Parameters.AddWithValue("@MinWithdrawalAmount", cryptocurrency.MinWithdrawalAmount);
                cmd.Parameters.AddWithValue("@WithdrawalFee", cryptocurrency.WithdrawalFee);
                cmd.Parameters.AddWithValue("@IconUrl", cryptocurrency.IconUrl);
                cmd.Parameters.AddWithValue("@ModifiedBy", cryptocurrency.ModifiedBy);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }
    }
}
