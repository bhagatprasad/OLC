using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class StateManager:IStateManager
    {
        private readonly string connectionString;
        public StateManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<State> GetStateByStateAsync(long stateId)
        {
            State getStateById = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetStatesByState]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@stateId", stateId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getStateById = new State();

                    getStateById.Id = Convert.ToInt64(item["Id"]);

                    getStateById.CountryId = item["CountryId"] != DBNull.Value ? Convert.ToInt64(item["CountryId"]) : null;

                    getStateById.Name = (item["Name"].ToString());

                    getStateById.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getStateById.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getStateById.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getStateById.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    getStateById.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getStateById.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return getStateById;
        }

        public async Task<List<State>> GetStatesByCountryAsync(long countryId)
        {
            List<State> getStates = new List<State>();

            State getStateByCountry = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetSatesByCountry]", connection);

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

                    getStateByCountry = new State();

                    getStateByCountry.Id = Convert.ToInt64(item["Id"]);

                    getStateByCountry.Name = item["Name"].ToString();

                    getStateByCountry.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getStateByCountry.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getStateByCountry.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getStateByCountry.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getStateByCountry.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getStates.Add(getStateByCountry);
                }
            }

            return getStates;
        }

        public async  Task<List<State>> GetStatesListAsync()
        {
            List<State> getStates = new List<State>();

            State getState = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetStates]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getState = new State();

                    getState.Id = Convert.ToInt64(item["Id"]);

                    getState.Name = item["Name"].ToString();

                    getState.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getState.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getState.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getState.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getState.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getStates.Add(getState);
                }
            }

            return getStates;
        }
    }
}
