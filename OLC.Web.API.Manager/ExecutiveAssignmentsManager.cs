using Newtonsoft.Json.Linq;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class ExecutiveAssignmentsManager : IExecutiveAssignmentsManager
    {

        private readonly string connectionString;
        public ExecutiveAssignmentsManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> AssignPaymentOrdersIntoExecutiveQueueAsync(PushPaymentOrderIntoQue pushPaymentOrderIntoQue)
        {
            string paymentOrderIds = string.Join(",", pushPaymentOrderIntoQue.PaymentOrderIds);

            if (pushPaymentOrderIntoQue != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspAssignPaymentOrdersIntoExecutiveQue]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@PaymentOrderIds", SqlDbType.NVarChar, -1).Value = paymentOrderIds;

                cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = pushPaymentOrderIntoQue.UserId;

                cmd.Parameters.Add("@ExecutiveId", SqlDbType.BigInt).Value = pushPaymentOrderIntoQue.ExecutiveId;

                cmd.Parameters.Add("@AssignedBy", SqlDbType.BigInt).Value = pushPaymentOrderIntoQue.AssignedBy;

                cmd.Parameters.Add("@AssignedAt", SqlDbType.DateTimeOffset);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;

        }

        public async Task<bool> DeleteExecutiveAssignmentsAsync(long id)
        {
            if (id != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteExecutiveAssignments]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@executiveId", id);            
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;

        }

        public async Task<bool> InsertExecutiveAssignmentsAsync(ExecutiveAssignments executiveAssignments)
        {
            if (executiveAssignments != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertExecutiveAssignments]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                
                sqlCommand.Parameters.AddWithValue("@userId", executiveAssignments.UserId);
                sqlCommand.Parameters.AddWithValue("@paymentOrderId", executiveAssignments.PaymentOrderId);
                sqlCommand.Parameters.AddWithValue("@executiveId", executiveAssignments.ExecutiveId);
                sqlCommand.Parameters.AddWithValue("@orderQueueId", executiveAssignments.OrderQueueId);
                sqlCommand.Parameters.AddWithValue("@startedAt", executiveAssignments.StartedAt);
                sqlCommand.Parameters.AddWithValue("@completedAt", executiveAssignments.CompletedAt);                
                sqlCommand.Parameters.AddWithValue("@notes", executiveAssignments.Notes);
                sqlCommand.Parameters.AddWithValue("@createdBy", executiveAssignments.CreatedBy);
                sqlCommand.ExecuteNonQuery();
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

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateExecutiveAssignments]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", executiveAssignments.Id);
                sqlCommand.Parameters.AddWithValue("@userId", executiveAssignments.UserId);
                sqlCommand.Parameters.AddWithValue("@paymentOrderId", executiveAssignments.PaymentOrderId);
                sqlCommand.Parameters.AddWithValue("@executiveId", executiveAssignments.ExecutiveId);
                sqlCommand.Parameters.AddWithValue("@orderQueueId", executiveAssignments.OrderQueueId);
                sqlCommand.Parameters.AddWithValue("@assignmentStatus", executiveAssignments.AssignmentStatus);
                sqlCommand.Parameters.AddWithValue("@assignedBy", executiveAssignments.AssignedBy);
                sqlCommand.Parameters.AddWithValue("@startedAt", executiveAssignments.StartedAt);
                sqlCommand.Parameters.AddWithValue("@completedAt", executiveAssignments.CompletedAt);
                sqlCommand.Parameters.AddWithValue("@notes", executiveAssignments.Notes);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", executiveAssignments.ModifiedBy);
                sqlCommand.Parameters.AddWithValue("@isActive", executiveAssignments.IsActive);
                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}
