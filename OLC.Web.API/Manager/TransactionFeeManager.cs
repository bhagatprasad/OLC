using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class TransactionFeeManager : ITransactionFeeManager
    { 
        private readonly string connectionString;
       
        public TransactionFeeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<TransactionFee> GetTransactionFeeByIdAsync(long feeId)
        {
            TransactionFee transactionFee = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetTransactionFeeById]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@feeId", feeId);
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
                    transactionFee.CreatedBy = dr["Price"] != DBNull.Value ? Convert.ToInt64(dr["Price"]) : null;
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
    }
}

