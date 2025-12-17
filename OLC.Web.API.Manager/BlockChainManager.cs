using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace OLC.Web.API.Manager
{
    public class BlockChainManager : IBlockChainManager
    {
        private readonly string connectionString;

        public BlockChainManager(IConfiguration configuration)
        {
           connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> DeleteBlockChainAsync(long id)
        {
            if (id != 0)
            {
                SqlConnection sqlConnection=new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteBlockChain]",sqlConnection);
                sqlCommand.CommandType=CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id",id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<BlockChain> GetBlockChainByIdAsync(long id)
        {
            BlockChain blockChain = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@blockChainId ",id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    blockChain = new BlockChain();

                    blockChain.Id=Convert.ToInt64(row["id"]);
                    blockChain.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : null;

                    blockChain.Code = row["Code"] != DBNull.Value ? row["Code"].ToString() : null;

                    blockChain.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;

                    blockChain.CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null;

                    blockChain.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;

                    blockChain.ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null;

                    blockChain.IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null;
                }
            }
            return blockChain;
        }

        public async Task<List<BlockChain>> GetBlockChainsAsync()
        {
            List<BlockChain> blockChains = new List<BlockChain>();

            BlockChain blockChain = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllBlockChains]",sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter (sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill (dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    blockChain = new BlockChain();

                    blockChain.Id = Convert.ToInt64(row["Id"]);

                    blockChain.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : null;

                    blockChain.Code = row["Code"] != DBNull.Value ? row["Code"].ToString() : null;

                    blockChain.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;

                    blockChain.CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null;

                    blockChain.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;

                    blockChain.ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null;

                    blockChain.IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null;

                    blockChains.Add(blockChain);
                }
            }
            return blockChains;
        }

        public async Task<bool> InsertBlockChainAsync(BlockChain blockChain)
        {
            if (blockChain != null)
            {
                SqlConnection sqlConnection= new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertBlockChain]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@name",blockChain.Name);
                sqlCommand.Parameters.AddWithValue("@code",blockChain.Code);
                sqlCommand.Parameters.AddWithValue("@createdBy",blockChain.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@IsActive", blockChain.IsActive);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateBlockChainAsync(BlockChain blockChain)
        {
            if(blockChain != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open ();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateBlockChain]", sqlConnection);
                sqlCommand.CommandType= CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id",blockChain.Id);
                sqlCommand.Parameters.AddWithValue("@name",blockChain.Name);
                sqlCommand.Parameters.AddWithValue("@code", blockChain.Code);
                sqlCommand.Parameters.AddWithValue("@IsActive", blockChain.IsActive);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", blockChain.ModifiedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}
