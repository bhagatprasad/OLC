using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class WalletTypeManager : IWalletTypeManager
    {
        private readonly string connectionString;

        public WalletTypeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> DeleteWalletTypeAsync(long id)
        {
            if (id != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteWalletTypes]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<List<WalletType>> GetAllWalletTypesAsync()
        {
            List<WalletType> walletTypes = new List<WalletType>();
            WalletType walletType = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetWalletTypes]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    walletType = new WalletType();

                    walletType.Id = Convert.ToInt64(row["Id"]);

                    walletType.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : null;

                    walletType.Code = row["Code"] != DBNull.Value ? row["Code"].ToString() : null;

                    walletType.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;

                    walletType.CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTime?)row["CreatedOn"] : null;

                    walletType.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;

                    walletType.ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTime?)row["ModifiedOn"] : null;

                    walletType.IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null;

                    walletTypes.Add(walletType);
                }
            }
            return walletTypes;
        }

        public async Task<WalletType> GetWalletTypeByIdAsync(long id)
        {
            WalletType walletType = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetWalletTypesById]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@walletTypeId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    walletType = new WalletType();

                    walletType.Id = Convert.ToInt64(row["Id"]);

                    walletType.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : null;

                    walletType.Code = row["Code"] != DBNull.Value ? row["Code"].ToString() : null;

                    walletType.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;

                    walletType.CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTime?)row["CreatedOn"] : null;

                    walletType.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;

                    walletType.ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTime?)row["ModifiedOn"] : null;

                    walletType.IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null;
                }
            }
            return walletType;
        }

        public async Task<bool> InsertWalletTypeAsync(WalletType walletType)
        {
            if (walletType != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertWalletTypes]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@name", walletType.Name);
                sqlCommand.Parameters.AddWithValue("@code", walletType.Code);
                sqlCommand.Parameters.AddWithValue("@createdBy", walletType.CreatedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateWalletTypeAsync(WalletType walletType)
        {
            if (walletType != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateWalletTypes]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", walletType.Id);
                sqlCommand.Parameters.AddWithValue("@name", walletType.Name);
                sqlCommand.Parameters.AddWithValue("@code", walletType.Code);
                sqlCommand.Parameters.AddWithValue("@IsActive", walletType.IsActive);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", walletType.ModifiedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}
