using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class CountryManager:ICountryManager
    {
        private readonly string connectionString;
        public CountryManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Country> GetCountryByCountryIdAsync(long countryId)
        {
            Country getcountryById = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetcountryById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@countryId", countryId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getcountryById = new Country();

                    getcountryById.Id = Convert.ToInt64(item["Id"]);

                    getcountryById.Name = (item["Name"].ToString());

                    getcountryById.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getcountryById.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getcountryById.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getcountryById.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    getcountryById.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getcountryById.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return getcountryById;
        }
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            List<Country> getCountries = new List<Country>();

            Country getCountry = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCountries]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getCountry = new Country();

                    getCountry.Id = Convert.ToInt64(item["Id"]);

                    getCountry.Name = item["Name"].ToString();

                    getCountry.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getCountry.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getCountry.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getCountry.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getCountry.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getCountries.Add(getCountry);
                }
            }

            return getCountries;
        }
        public async Task<bool> InsertCountryAsync(Country country)
        {
            if (country != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertCountries]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@name", country.Name);

                sqlCommand.Parameters.AddWithValue("@code", country.Code);

                sqlCommand.Parameters.AddWithValue("@createdBy", country.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            if (country != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateCountries]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", country.Id);

                sqlCommand.Parameters.AddWithValue("@name", country.Name);

                sqlCommand.Parameters.AddWithValue("@code", country.Code);

                sqlCommand.Parameters.AddWithValue("@modifiedBy", country.ModifiedBy);

                sqlCommand.Parameters.AddWithValue("@isActive", country.IsActive);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCountryAsync(long countryId)
        {
            if (countryId != 0)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteCountry]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@countryId " ,countryId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }
}
