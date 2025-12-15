using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class OrderQueueManager : IOrderQueueManager
    {
        private readonly string _connectionString;

        public OrderQueueManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> InsertOrderQueueAsync(OrderQueue orderQueue)
        {
            if (orderQueue != null)
            {

                SqlConnection sqlConnection = new SqlConnection(_connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspInsertOrderQueue]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PaymentOrderId", orderQueue.PaymentOrderId);
               
                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateOrderQueueAsync(OrderQueue orderQueue)
        {
            if (orderQueue != null)
            {

                SqlConnection sqlConnection = new SqlConnection(_connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspUpdateOrderQueue]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PaymentOrderId", orderQueue.PaymentOrderId);
                cmd.Parameters.AddWithValue("@OrderReference", orderQueue.OrderReference);
                cmd.Parameters.AddWithValue("@Priority", orderQueue.Priority);
                cmd.Parameters.AddWithValue("@Metadata", (object?)orderQueue.Metadata ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedBy", orderQueue.PaymentOrderId);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteOrderQueueAsync(long orderQueueId)
        {
            if (orderQueueId != 0) ;
            {

                SqlConnection sqlConnection = new SqlConnection(_connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspUpdateOrderQueue]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderQueueId", orderQueueId);
                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }

        public async Task<List<OrderQueue>> GetOrderQueuesAsync()
        {
            List<OrderQueue> orderQueues = new List<OrderQueue>();

            OrderQueue orderQueue = null;

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetOrderQueue]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    orderQueue = new OrderQueue();

                    orderQueue.Id = Convert.ToInt64(item["Id"]);

                    orderQueue.PaymentOrderId = Convert.ToInt64(item["PaymentOrderId"]);

                    orderQueue.OrderReference = item["OrderReference"] != DBNull.Value ? item["OrderReference"].ToString() : null;

                    orderQueue.QueueStatus = item["QueueStatus"] != DBNull.Value ? item["QueueStatus"].ToString() : null;

                    orderQueue.Priority = item["Priority"] != DBNull.Value ? Convert.ToInt32(item["Priority"]) : 0;

                    orderQueue.AssignedExecutiveId = item["AssignedExecutiveId"] != DBNull.Value ? Convert.ToInt64(item["AssignedExecutiveId"]) : null;

                    orderQueue.AssignedOn = item["AssignedOn"] != DBNull.Value ? (DateTimeOffset?)item["AssignedOn"] : null;

                    orderQueue.ProcessingStartedOn = item["ProcessingStartedOn"] != DBNull.Value ? (DateTimeOffset?)item["ProcessingStartedOn"] : null;

                    orderQueue.ProcessingCompletedOn = item["ProcessingCompletedOn"] != DBNull.Value ? (DateTimeOffset?)item["ProcessingCompletedOn"] : null;

                    orderQueue.RetryCount = item["RetryCount"] != DBNull.Value ? Convert.ToInt32(item["RetryCount"]) : 0;

                    orderQueue.MaxRetries = item["MaxRetries"] != DBNull.Value ? Convert.ToInt32(item["MaxRetries"]) : 0;

                    orderQueue.FailureReason = item["FailureReason"] != DBNull.Value ? item["FailureReason"].ToString() : null;

                    orderQueue.Metadata = item["Metadata"] != DBNull.Value ? item["Metadata"].ToString() : null;

                    orderQueue.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    orderQueue.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    orderQueue.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    orderQueue.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    orderQueue.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    orderQueues.Add(orderQueue);
                }
            }

            return orderQueues;
        }
    }
}
