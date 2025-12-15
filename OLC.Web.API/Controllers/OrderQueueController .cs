using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderQueueController : ControllerBase
    {
        private readonly IOrderQueueManager _orderQueueManager;

        public OrderQueueController(IOrderQueueManager orderQueueManager)
        {
            _orderQueueManager = orderQueueManager;
        }
        public async Task<bool> InsertOrderQueueAsync(OrderQueue orderQueue)
        {
            if (orderQueue != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertOrderQueue]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@PaymentOrderId", orderQueue.PaymentOrderId);
                sqlCommand.Parameters.AddWithValue("@OrderReference", orderQueue.OrderReference);
                sqlCommand.Parameters.AddWithValue("@Priority", orderQueue.Priority);
                sqlCommand.Parameters.AddWithValue("@Metadata", (object?)orderQueue.Metadata ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", orderQueue.CreatedBy);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return true;
            }
            return false;
        }
        public async Task<bool> UpdateOrderQueueAsync(OrderQueue orderQueue)
        {
            if (orderQueue != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateOrderQueue]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@OrderQueueId", orderQueue.Id);
                sqlCommand.Parameters.AddWithValue("@QueueStatus", orderQueue.QueueStatus);
                sqlCommand.Parameters.AddWithValue("@AssignedExecutiveId",
                    (object?)orderQueue.AssignedExecutiveId ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Priority", orderQueue.Priority);
                sqlCommand.Parameters.AddWithValue("@FailureReason",
                    (object?)orderQueue.FailureReason ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Metadata",
                    (object?)orderQueue.Metadata ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ModifiedBy", orderQueue.ModifiedBy);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteOrderQueueAsync(long orderQueueId)
        {
            if (orderQueueId != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteOrderQueue]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@OrderQueueId", orderQueueId);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }
}
