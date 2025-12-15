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

        public async Task<long> InsertOrderQueueAsync(OrderQueue orderQueue)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("uspInsertOrderQueue", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PaymentOrderId", orderQueue.PaymentOrderId);
            cmd.Parameters.AddWithValue("@OrderReference", orderQueue.OrderReference);
            cmd.Parameters.AddWithValue("@Priority", orderQueue.Priority);
            cmd.Parameters.AddWithValue("@Metadata", (object?)orderQueue.Metadata ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedBy", orderQueue.PaymentOrderId); // replace with UserId if available

            await conn.OpenAsync();
            return Convert.ToInt64(await cmd.ExecuteScalarAsync());
        }

        public async Task<bool> UpdateOrderQueueAsync(OrderQueue orderQueue)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("uspUpdateOrderQueue", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OrderQueueId", orderQueue.Id);
            cmd.Parameters.AddWithValue("@QueueStatus", orderQueue.QueueStatus);
            cmd.Parameters.AddWithValue("@AssignedExecutiveId",
                (object?)orderQueue.AssignedExecutiveId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Priority", orderQueue.Priority);
            cmd.Parameters.AddWithValue("@FailureReason",
                (object?)orderQueue.FailureReason ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Metadata",
                (object?)orderQueue.Metadata ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ModifiedBy",
                orderQueue.AssignedExecutiveId ?? 0);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> DeleteOrderQueueAsync(long orderQueueId, string reason)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("uspSoftDeleteOrderQueue", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OrderQueueId", orderQueueId);
            cmd.Parameters.AddWithValue("@Reason", reason);
            cmd.Parameters.AddWithValue("@ModifiedBy", orderQueueId);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return true;
        }
    }
}
