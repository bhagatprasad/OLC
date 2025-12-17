using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class TransactionFeeManager : ITransactionFeeManager
    { 
        private readonly string connectionString;
       
        public TransactionFeeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> DeleteTransactionFeeAsync(long transactionFeeId)
        {

            if (transactionFeeId != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteTransactionFee]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@transactionFeeId", transactionFeeId);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<TransactionFee> GetTransactionFeeByIdAsync(long transactionFeeId)
        {
            TransactionFee transactionFee = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetTransactionFeeById]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@transactionFeeId", transactionFeeId);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    transactionFee = new TransactionFee();

                    transactionFee.Id = Convert.ToInt64(dr["Id"]);
                    transactionFee.Name = dr["Name"] != DBNull.Value ? Convert.ToString(dr["Name"]) : null;
                    transactionFee.Code = dr["Code"] != DBNull.Value ? Convert.ToString(dr["Code"]) : null;
                    transactionFee.CreatedBy = dr["Price"] != DBNull.Value ? Convert.ToInt64(dr["Price"]) : null;
                    transactionFee.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    transactionFee.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    transactionFee.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    transactionFee.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    transactionFee.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : null;
                }
            }
            return transactionFee;
        }

        public async Task<List<TransactionFee>> GetTransactionFeesListAsync()
        {
            List<TransactionFee> transactionFees = new List<TransactionFee>();

            TransactionFee transactionFee = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllTransactionFeesDetails]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    transactionFee = new TransactionFee();

                    transactionFee.Id = Convert.ToInt64(dr["Id"]);
                    transactionFee.Name = dr["Name"] != DBNull.Value ? Convert.ToString(dr["Name"]) : null;
                    transactionFee.Code = dr["Code"] != DBNull.Value ? Convert.ToString(dr["Code"]) : null;
                    transactionFee.Price = dr["Price"] != DBNull.Value ? Convert.ToInt64(dr["Price"]) : null;
                    transactionFee.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    transactionFee.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    transactionFee.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    transactionFee.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    transactionFee.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : null;

                    transactionFees.Add(transactionFee);
                }

            }
            return transactionFees;
        }

        public async Task<bool> InsertTransactionFeeAsync(TransactionFee transactionFee)
        {

            if (transactionFee != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertTransactionFee]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@name", transactionFee.Name);
                sqlCommand.Parameters.AddWithValue("@code", transactionFee.Code);
                sqlCommand.Parameters.AddWithValue("@price", transactionFee.Price);
                sqlCommand.Parameters.AddWithValue("@createdBy", transactionFee.CreatedBy);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTransactionFeeAsync(TransactionFee transactionFee)
        {
            if (transactionFee != null)
            {
                  
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateTransactionFee]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@transactionFeeId", transactionFee.Id);
                sqlCommand.Parameters.AddWithValue("@name", transactionFee.Name);
                sqlCommand.Parameters.AddWithValue("@code", transactionFee.Code);
                sqlCommand.Parameters.AddWithValue("@price", transactionFee.Price);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", transactionFee.ModifiedBy);
                sqlCommand.Parameters.AddWithValue("@isActive", transactionFee.IsActive);


                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}

