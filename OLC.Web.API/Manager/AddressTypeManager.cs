
using Microsoft.AspNetCore.Http.HttpResults;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class AddressTypeManager : IAddressTypeManager
    {
        private readonly string connectionString;

        public AddressTypeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<AddressType> GetUserAdressTypeByIdAsync(long addressTypeId)
        {
            AddressType getAddressTypeById = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAddressTypeById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@addressTypeId", addressTypeId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getAddressTypeById = new AddressType();

                    getAddressTypeById.Id = Convert.ToInt64(item["Id"]);

                    getAddressTypeById.Name = (item["Name"].ToString());

                    getAddressTypeById.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getAddressTypeById.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getAddressTypeById.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getAddressTypeById.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getAddressTypeById.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return getAddressTypeById;
        }

        public async Task<List<AddressType>> GetUserAddressTypeAsync()
        {
            List<AddressType> getAddressTypes = new List<AddressType>();

            AddressType getAddressType = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAddressTypes]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getAddressType = new AddressType();

                    getAddressType.Id = Convert.ToInt64(item["Id"]);

                    getAddressType.Name = item["Name"].ToString();

                    getAddressType.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getAddressType.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getAddressType.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getAddressType.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getAddressType.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getAddressTypes.Add(getAddressType);
                }
            }

            return getAddressTypes;
        }
        public async Task<bool> InsertUserAddressTypeAsync(AddressType addressType)
        {
            if (addressType != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertAddressType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@name", addressType.Name);

                sqlCommand.Parameters.AddWithValue("@code", addressType.Code);

                sqlCommand.Parameters.AddWithValue("@createdBy", addressType.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateUserAddressTypeAsync(AddressType addressType)
        {
            if (addressType != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateAddressType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", addressType.Id);

                sqlCommand.Parameters.AddWithValue("@name", addressType.Name);

                sqlCommand.Parameters.AddWithValue("@code", addressType.Code);

                sqlCommand.Parameters.AddWithValue("@modifiedBy", addressType.ModifiedBy);

                sqlCommand.Parameters.AddWithValue("@isActive", addressType.IsActive);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }
        public async Task<bool> DeleteUserAddressTypeAsync(long Id)
        {
            if (Id != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteAddressType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", Id);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }    
}
