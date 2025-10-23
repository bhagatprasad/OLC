using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class BankManager : IBankManager
    {
        private readonly string connectionString;

        public BankManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<bool> UpdateBankAsync(Bank bank)
        {
            if (bank != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateBank]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", bank.Id);
                sqlCommand.Parameters.AddWithValue("@name", bank.Name);
                sqlCommand.Parameters.AddWithValue("@code", bank.Code);
                sqlCommand.Parameters.AddWithValue("@isActive", bank.IsActive);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }


        public async Task<bool> DeleteBankAsync(long Id)
        {
            if (Id != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteBank]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", Id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }        
        public async Task<bool> InsertBankAsync(Bank bank)
        {
            if (bank != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertBank]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@name", bank.Name);
                sqlCommand.Parameters.AddWithValue("@code", bank.Code);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", bank.CreatedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<Bank> GetBankByIdAsync(long id)
        {
            Bank bank = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetBankById]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
           
            DataTable dt = new DataTable();
           
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();

            if(dt.Rows.Count > 0)
            {
               foreach(DataRow dr in dt.Rows)
                {
                    bank = new Bank();
                    bank.Id = Convert.ToInt64(dr["Id"]);
                    bank.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null;
                    bank.Code = dr["Code"] != DBNull.Value ? dr["Code"].ToString() : null;
                    bank.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    bank.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    bank.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    bank.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    bank.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : null;

                }
            }
            return bank;
        }

        public async Task<List<Bank>> GetBankAsync()
        {
            List<Bank> banks = new List<Bank>();
           
            Bank bank = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetBank]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);            
            DataTable dt = new DataTable();

            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    bank = new Bank();
                    bank.Id = Convert.ToInt64(dr["Id"]);
                    bank.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null;
                    bank.Code = dr["Code"] != DBNull.Value ? dr["Code"].ToString() : null;
                    bank.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    bank.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    bank.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    bank.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    bank.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : null;

                    banks.Add(bank);
                }               
            }
            return banks;
        }
    }   
}
