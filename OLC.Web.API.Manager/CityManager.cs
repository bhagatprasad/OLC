using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace OLC.Web.API.Manager
{
    public class CityManager : ICityManager
    {
        private readonly string connectionString;
        public CityManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<City>> GetCitiesListAsync()
        {
            List<City> cities = new List<City>();

            City city = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCities]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    city = new City();

                    city.Id = Convert.ToInt64(dr["Id"]);
                    city.CountryId = dr["CountryId"] != DBNull.Value ? Convert.ToInt64(dr["CountryId"]) : null;
                    city.StateId = dr["StateId"] != DBNull.Value ? Convert.ToInt64(dr["StateId"]) : null;
                    city.Name = dr["Name"] != DBNull.Value ? Convert.ToString(dr["Name"]) : null;
                    city.Code = dr["Code"] != DBNull.Value ? Convert.ToString(dr["Code"]) : null;
                    city.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    city.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    city.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    city.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    city.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : null;

                    cities.Add(city);
                }

            }
            return cities;
        }

        public async Task<List<City>> GetCitiesByCountryAsync(long countryId)
        {
            List<City> cities = new List<City>();

            City city = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCitiesByCountry]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@countryId", countryId);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    city = new City();

                    city.Id = Convert.ToInt64(dr["Id"]);
                    city.CountryId = dr["CountryId"] != DBNull.Value ? Convert.ToInt64(dr["CountryId"]) : null;
                    city.StateId = dr["StateId"] != DBNull.Value ? Convert.ToInt64(dr["StateId"]) : null;
                    city.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null;
                    city.Code = dr["Code"] != DBNull.Value ? dr["Code"].ToString() : null;
                    city.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    city.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)(dr["CreatedOn"]) : null;
                    city.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    city.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)(dr["ModifiedOn"]) : null;
                    city.IsActive = dr["IsActive"] != DBNull.Value ? (bool)(dr["IsActive"]) : null;
                    cities.Add(city);
                }
            }
            return cities;
        }

        public async Task<List<City>> GetCitiesByStateAsync(long stateId)
        {
            List<City> cities = new List<City>();
            City city = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCitiesByState]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@stateId", stateId);

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    city = new City();
                    city.Id = Convert.ToInt64(dr["Id"]);
                    city.CountryId = dr["CountryId"] != DBNull.Value ? Convert.ToInt64(dr["CountryId"]) : null;
                    city.StateId = dr["StateId"] != DBNull.Value ? Convert.ToInt64(dr["StateId"]) : null;
                    city.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null;
                    city.Code = dr["Code"] != DBNull.Value ? dr["Code"].ToString() : null;
                    city.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    city.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)(dr["CreatedOn"]) : null;
                    city.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    city.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)(dr["ModifiedOn"]) : null;
                    city.IsActive = dr["IsActive"] != DBNull.Value ? (bool)(dr["IsActive"]) : null;
                    cities.Add(city);
                }
            }
            return (cities);
        }

        public async Task<City> GetCityByIdAsync(long cityId)
        {
            City city = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCityById]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@cityId", cityId);

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    city = new City();
                    city.Id = Convert.ToInt64(dr["Id"]);
                    city.CountryId = dr["CountryId"] != DBNull.Value ? Convert.ToInt64(dr["CountryId"]) : null;
                    city.StateId = dr["StateId"] != DBNull.Value ? Convert.ToInt64(dr["StateId"]) : null;
                    city.Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null;
                    city.Code = dr["Code"] != DBNull.Value ? dr["Code"].ToString() : null;
                    city.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    city.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)(dr["CreatedOn"]) : null;
                    city.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    city.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)(dr["ModifiedOn"]) : null;
                    city.IsActive = dr["IsActive"] != DBNull.Value ? (bool)(dr["IsActive"]) : null;

                }
            }
            return city;
        }
    }
}