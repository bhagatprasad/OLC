using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class ExecutiveAssignmentsManager : IExecutiveAssignmentsManager
    {

        private readonly string connectionString;
        public ExecutiveAssignmentsManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> InsertExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments)
        {
            if (executiveAssignments != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertExecutiveAssignments]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ExecutiveId", executiveAssignments.ExecutiveId);

                sqlCommand.Parameters.AddWithValue("@OrderQueueId", executiveAssignments.OrderQueueId);               

                sqlCommand.Parameters.AddWithValue("@Notes", executiveAssignments.Notes);
                sqlConnection.Close();

                return true;
            }
            return false;
        }

        public async Task<bool> UpdateExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments)
        {
            if (executiveAssignments != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertExecutiveAssignments]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ExecutiveId", executiveAssignments.ExecutiveId);

                sqlCommand.Parameters.AddWithValue("@OrderQueueId", executiveAssignments.OrderQueueId);             

                sqlCommand.Parameters.AddWithValue("@Notes", executiveAssignments.Notes);
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}
