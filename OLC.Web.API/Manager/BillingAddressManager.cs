using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class BillingAddressManager : IBillingAddressManager
    {
        private readonly string connectionString;
        public BillingAddressManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<UserBillingAddress> GetUserBillingAddressByIdAsync(long billingAddressId)
        {
            UserBillingAddress userBillingAddress = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserBillingAddress]", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@billingAddressId", billingAddressId);

                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            userBillingAddress = new UserBillingAddress();

                            userBillingAddress.Id = Convert.ToInt64(item["Id"]);
                            userBillingAddress.UserId = Convert.ToInt64(item["UserId"]);
                            userBillingAddress.AddessLineOne = item["AddessLineOne"] != DBNull.Value ? item["AddessLineOne"].ToString() : null;
                            userBillingAddress.AddessLineTwo = item["AddessLineTwo"] != DBNull.Value ? item["AddessLineTwo"].ToString() : null;
                            userBillingAddress.AddessLineThress = item["AddessLineThress"] != DBNull.Value ? item["AddessLineThress"].ToString() : null;
                            userBillingAddress.Location = item["Location"] != DBNull.Value ? item["Location"].ToString() : null;
                            userBillingAddress.CountryId = item["CountryId"] != DBNull.Value ? Convert.ToInt64(item["CountryId"]) : null;
                            userBillingAddress.StateId = item["StateId"] != DBNull.Value ? Convert.ToInt64(item["StateId"]) : null;
                            userBillingAddress.CityId = item["CityId"] != DBNull.Value ? Convert.ToInt64(item["CityId"]) : null;
                            userBillingAddress.PinCode = item["PinCode"] != DBNull.Value ? item["PinCode"].ToString() : null;
                            userBillingAddress.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                            userBillingAddress.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                            userBillingAddress.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                            userBillingAddress.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                            userBillingAddress.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                        }
                    }
                }
            }

            return userBillingAddress;
        }

        public async Task<List<UserBillingAddress>> GetUserBillingAddressesAsync(long userId)
        {
            List<UserBillingAddress> userBillingAddresses = new List<UserBillingAddress>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUserBillingAddress]", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@userId", userId);

                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            UserBillingAddress userBillingAddress = new UserBillingAddress();

                            userBillingAddress.Id = Convert.ToInt64(item["Id"]);
                            userBillingAddress.UserId = Convert.ToInt64(item["UserId"]);
                            userBillingAddress.AddessLineOne = item["AddessLineOne"] != DBNull.Value ? item["AddessLineOne"].ToString() : null;
                            userBillingAddress.AddessLineTwo = item["AddessLineTwo"] != DBNull.Value ? item["AddessLineTwo"].ToString() : null;
                            userBillingAddress.AddessLineThress = item["AddessLineThress"] != DBNull.Value ? item["AddessLineThress"].ToString() : null;
                            userBillingAddress.Location = item["Location"] != DBNull.Value ? item["Location"].ToString() : null;
                            userBillingAddress.CountryId = item["CountryId"] != DBNull.Value ? Convert.ToInt64(item["CountryId"]) : null;
                            userBillingAddress.StateId = item["StateId"] != DBNull.Value ? Convert.ToInt64(item["StateId"]) : null;
                            userBillingAddress.CityId = item["CityId"] != DBNull.Value ? Convert.ToInt64(item["CityId"]) : null;
                            userBillingAddress.PinCode = item["PinCode"] != DBNull.Value ? item["PinCode"].ToString() : null;
                            userBillingAddress.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                            userBillingAddress.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                            userBillingAddress.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                            userBillingAddress.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                            userBillingAddress.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                            userBillingAddresses.Add(userBillingAddress);
                        }
                    }
                }
            }

            return userBillingAddresses;
        }

        public async Task<bool> InsertUserBillingAddressAsync(UserBillingAddress userBillingAddress)
        {
            if (userBillingAddress != null)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();

                    SqlCommand sqlCommand = new SqlCommand("[dbo].[uspSaveUserBillingAddress]", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@userId", userBillingAddress.UserId);
                    sqlCommand.Parameters.AddWithValue("@addessLineOne", userBillingAddress.AddessLineOne);
                    sqlCommand.Parameters.AddWithValue("@addessLineTwo", userBillingAddress.AddessLineTwo);
                    sqlCommand.Parameters.AddWithValue("@addessLineThress", userBillingAddress.AddessLineThress);
                    sqlCommand.Parameters.AddWithValue("@location", userBillingAddress.Location);
                    sqlCommand.Parameters.AddWithValue("@countryId", userBillingAddress.CountryId);
                    sqlCommand.Parameters.AddWithValue("@stateId", userBillingAddress.StateId);
                    sqlCommand.Parameters.AddWithValue("@cityId", userBillingAddress.CityId);
                    sqlCommand.Parameters.AddWithValue("@pinCode", userBillingAddress.PinCode);
                    await sqlCommand.ExecuteNonQueryAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateUserBillingAddressAsync(UserBillingAddress userBillingAddress)
        {
            if (userBillingAddress != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateUserBillingAddress]", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@id", userBillingAddress.Id);
                    sqlCommand.Parameters.AddWithValue("@userId", userBillingAddress.UserId);
                    sqlCommand.Parameters.AddWithValue("@addessLineOne", userBillingAddress.AddessLineOne);
                    sqlCommand.Parameters.AddWithValue("@addessLineTwo", userBillingAddress.AddessLineTwo);
                    sqlCommand.Parameters.AddWithValue("@addessLineThress", userBillingAddress.AddessLineThress);
                    sqlCommand.Parameters.AddWithValue("@location", userBillingAddress.Location);
                    sqlCommand.Parameters.AddWithValue("@countryId", userBillingAddress.CountryId);
                    sqlCommand.Parameters.AddWithValue("@stateId", userBillingAddress.StateId);
                    sqlCommand.Parameters.AddWithValue("@cityId", userBillingAddress.CityId);
                    sqlCommand.Parameters.AddWithValue("@pinCode", userBillingAddress.PinCode);
                    await sqlCommand.ExecuteNonQueryAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteUserBillingAddressAsync(long billingAddressId)
        {
            if (billingAddressId != 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();

                    SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteUserBillingAddress]", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@billingAddressId", billingAddressId);

                    await sqlCommand.ExecuteNonQueryAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
