using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class ExecutivesManager : IExecutivesManager
    {
        private readonly string connectionString;
        public ExecutivesManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Executives>> GetAllExecutivesAsync()
        {
            List<Executives> executives = new List<Executives>();

            Executives ex = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllExecutives]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    ex = new Executives();

                    ex.Id = Convert.ToInt64(dr["Id"]);
                    ex.UserId = Convert.ToInt64(dr["UserId"]);
                    ex.FirstName = dr["FirstName"]?.ToString();
                    ex.LastName = dr["LastName"]?.ToString();
                    ex.Email = dr["Email"]?.ToString();
                    ex.MaxConcurrentOrders = dr["MaxConcurrentOrders"] != DBNull.Value ? Convert.ToInt32(dr["MaxConcurrentOrders"]) : null;
                    ex.IsAvailable = dr["IsAvailable"] != DBNull.Value ? Convert.ToBoolean(dr["IsAvailable"]) :null;
                    ex.CurrentOrderCount = dr["CurrentOrderCount"] != DBNull.Value ? Convert.ToInt32(dr["CurrentOrderCount"]) : null;
                    ex.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    ex.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    ex.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    ex.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    ex.IsActive = dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : null;

                    executives.Add(ex);
                }
            }

            return executives;
        }

        public async Task<Executives> GetExecutiveByUserId(long userId)
        {
            Executives ex = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetExecutiveByUserId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    ex = new Executives();

                    ex.Id = Convert.ToInt64(dr["Id"]);
                    ex.UserId = Convert.ToInt64(dr["UserId"]);
                    ex.FirstName = dr["FirstName"]?.ToString();
                    ex.LastName = dr["LastName"]?.ToString();
                    ex.Email = dr["Email"]?.ToString();
                    ex.MaxConcurrentOrders = dr["MaxConcurrentOrders"] != DBNull.Value ? Convert.ToInt32(dr["MaxConcurrentOrders"]) : null;
                    ex.IsAvailable = dr["IsAvailable"] != DBNull.Value ? Convert.ToBoolean(dr["IsAvailable"]) : null;
                    ex.CurrentOrderCount = dr["CurrentOrderCount"] != DBNull.Value ? Convert.ToInt32(dr["CurrentOrderCount"]) : null;
                    ex.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;
                    ex.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                    ex.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;
                    ex.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    ex.IsActive = dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : null;

                }
            }
            return ex;
        }

        public async Task<bool> InsertExecutuveAsyncs(Executives executive)
        {
            if (executive != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspInsertExecutive]", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", executive.UserId);
                cmd.Parameters.AddWithValue("@FirstName",executive.FirstName );
                cmd.Parameters.AddWithValue("@LastName", executive.LastName);
                cmd.Parameters.AddWithValue("@Email", executive.Email);
                cmd.Parameters.AddWithValue("@MaxConcurrentOrders", executive.MaxConcurrentOrders);
                cmd.Parameters.AddWithValue("@IsAvailable", executive.IsAvailable);
                cmd.Parameters.AddWithValue("@CurrentOrderCount", executive.CurrentOrderCount);

                cmd.ExecuteNonQuery();
                sqlConnection.Close();

                return true;
            }

            return false;
        }
    }
}
