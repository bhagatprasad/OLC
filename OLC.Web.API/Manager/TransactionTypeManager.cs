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

        public async Task<bool> deleteTransactionTypeAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID", nameof(id));

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteTransactionType]", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    
                    int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

                    
                    return rowsAffected > 0;
                }
                return false;
            }
        }

        public async Task <List<TransactionType>> GetTransactionTypeAsync()
        {
            List<TransactionType> transactionTypes = new List<TransactionType>();

            using(SqlConnection connection=new SqlConnection(connectionString)) 
            {
                await connection.OpenAsync();

                using (SqlCommand sqlcommand = new SqlCommand("[dbo].[uspGetTransactionType]", connection))
                {
                    sqlcommand.CommandType = System.Data.CommandType.StoredProcedure;

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

        public async Task<TransactionType?> GetTransactionTypeByIdAsync(long id)
        {
            TransactionType? transactionType = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetTransactionTypeById]", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            transactionType = new TransactionType
                            {
                                Id = reader["ID"] != DBNull.Value ? Convert.ToInt64(reader["ID"]) : 0,
                                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : null,
                                Code = reader["Code"] != DBNull.Value ? reader["Code"].ToString() : null,
                                CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt64(reader["CreatedBy"]) : null,
                                CreatedOn = reader["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)reader["CreatedOn"] : null,
                                ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(reader["ModifiedBy"]) : null,
                                ModifiedOn = reader["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)reader["ModifiedOn"] : null,
                                IsActive = reader["IsActive"] != DBNull.Value ? (bool?)reader["IsActive"] : null
                            };
                        }
                    }
                }
            }

            return transactionType;
        }


        public async Task<TransactionType> insertTransactionTypeAsync(TransactionType transactionType)
        {
            if (transactionType == null)
                throw new ArgumentNullException(nameof(transactionType));

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertTransactionTypes]", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@id", transactionType.Id);
                    sqlCommand.Parameters.AddWithValue("@name", (object?)transactionType.Name ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@code", (object?)transactionType.Code ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@createdBy", (object?)transactionType.CreatedBy ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@createdOn", (object?)transactionType.CreatedOn ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@modifiedBy", (object?)transactionType.ModifiedBy ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@modifiedOn", (object?)transactionType.ModifiedOn ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@isActive", (object?)transactionType.IsActive ?? DBNull.Value);

                    // If your SP returns the new ID, use ExecuteScalarAsync
                    var newId = await sqlCommand.ExecuteScalarAsync();
                    if (newId != null && newId != DBNull.Value)
                    {
                        transactionType.Id = Convert.ToInt64(newId);
                    }
                    else
                    {
                        // Otherwise, just execute non-query
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
            }

            return transactionType;
        }
            

        public async Task<TransactionType> updateTransactionTypeAsync(TransactionType transactionType)
        {
            if (transactionType == null)
                throw new ArgumentNullException(nameof(transactionType));

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateTransactionType]", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    // Add parameters with null-checks
                    sqlCommand.Parameters.AddWithValue("@id", transactionType.Id);
                    sqlCommand.Parameters.AddWithValue("@name", (object?)transactionType.Name ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@code", (object?)transactionType.Code ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@modifiedBy", (object?)transactionType.ModifiedBy ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@modifiedOn", (object?)transactionType.ModifiedOn ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@isActive", (object?)transactionType.IsActive ?? DBNull.Value);

                    // Execute the stored procedure
                    await sqlCommand.ExecuteNonQueryAsync();
                }
            }

            // Return the updated object (you can also return a bool if preferred)
            return transactionType;
        }
    }
}


