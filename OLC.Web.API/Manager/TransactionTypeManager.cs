using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class TransactionTypeManager : ITransactionTypeManager
    {
        private readonly string connectionString;
        public TransactionTypeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> DeleteTransactionTypeAsync(long id)
        {
           if(id!=0)
            {
                SqlConnection sqlConnection=new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteTransationTypes]", sqlConnection);

                sqlCommand.CommandType=CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id",id);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
           return false;
        }

        public async Task <List<TransactionType>> GetTransactionTypeAsync()
        {
            List<TransactionType> transactionTypes = new List<TransactionType>();

            using(SqlConnection connection=new SqlConnection(connectionString)) 
            {
                await connection.OpenAsync();

                using (SqlCommand sqlcommand = new SqlCommand("[dbo].[uspGetAllTransactionTypes]", connection))
                {
                    sqlcommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcommand))
                    {
                        DataTable dt = new DataTable();
                        sqlDataAdapter.Fill(dt);

                        if(dt.Rows.Count > 0 )
                        {
                           foreach(DataRow item in dt.Rows)
                            {
                                TransactionType transactionType = new TransactionType();

                                transactionType.Id=Convert.ToInt64(item["ID"]);
                                transactionType.Name = item["Name"] != DBNull.Value ? item["Name"].ToString() : null;
                                transactionType.Code = item["Code"]!=DBNull.Value? item["Code"].ToString() : null;
                                transactionType.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                                transactionType.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset)item["CreatedOn"] : null;
                                transactionType.CreatedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                                transactionType.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset) item["ModifiedOn"] : null;
                                transactionType.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                                transactionTypes.Add(transactionType);
                            }
                        }
                    }
                }
            }

            return transactionTypes;
        }

        public async Task<TransactionType> GetTransactionTypeByIdAsync(long id)
        {
            TransactionType transactionType = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetTransactionTypeById]", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dt=new DataTable();

                        sqlDataAdapter.Fill(dt);

                        sqlConnection.Close();

                        if(dt.Rows.Count > 0)
                        {
                            foreach(DataRow item in dt.Rows)
                            {
                                transactionType = new TransactionType();

                                transactionType.Id=Convert.ToInt64(item["Id"]);
                                transactionType.Name = item["Name"] != DBNull.Value ? item["Name"].ToString() : null;
                                transactionType.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;
                                transactionType.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                                transactionType.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                                transactionType.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                                transactionType.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                                transactionType.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                            }
                        }
                    }
                }
            }

            return transactionType;
        }


        public async Task<bool> InsertTransactionTypeAsync(TransactionType transactionType)
        {
            if (transactionType != null)
            {
                SqlConnection sqlConnection=new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand=new SqlCommand("[dbo].[uspInsertTransationTypes]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@name",transactionType.Name)
                    ;
                sqlCommand.Parameters.AddWithValue("@code",transactionType.Code);

                sqlCommand.Parameters.AddWithValue("@CreatedBy",transactionType.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            
            return false;
        }
            
        public async Task<bool> UpdateTransactionTypeAsync(TransactionType transactionType)
        {
            if (transactionType != null) 
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateTransationTypes]", sqlConnection);

                sqlCommand.CommandType= CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id",transactionType.Id);

                sqlCommand.Parameters.AddWithValue("@name", transactionType.Name);

                sqlCommand.Parameters.AddWithValue("@code",transactionType.Code);

                sqlCommand.Parameters.AddWithValue("@createdBy", transactionType.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false ;
        }
    }
}


