using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class QueueProcessingHistoryManager : IQueueProcessingHistoryManager
    {
        private readonly string _connectionString;

        public QueueProcessingHistoryManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<long> InsertHistoryAsync(QueueProcessingHistory history)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("uspInsertQueueProcessingHistory", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OrderQueueId", history.OrderQueueId);
            cmd.Parameters.AddWithValue("@ExecutiveId", (object?)history.ExecutiveId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FromStatus", (object?)history.FromStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToStatus", history.ToStatus);
            cmd.Parameters.AddWithValue("@Action", history.Action);
            cmd.Parameters.AddWithValue("@Details", (object?)history.Details ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IPAddress", (object?)history.IPAddress ?? DBNull.Value);

            await conn.OpenAsync();
            return Convert.ToInt64(await cmd.ExecuteScalarAsync());
        }

        public async Task<List<QueueProcessingHistory>> GetHistoryByOrderAsync(long orderQueueId)
        {
            var list = new List<QueueProcessingHistory>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("uspGetQueueProcessingHistoryByOrder", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderQueueId", orderQueueId);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new QueueProcessingHistory
                {
                    Id = reader.GetInt64(0),
                    OrderQueueId = reader.GetInt64(1),
                    ExecutiveId = reader.IsDBNull(2) ? null : reader.GetInt64(2),
                    FromStatus = reader.IsDBNull(3) ? null : reader.GetString(3),
                    ToStatus = reader.GetString(4),
                    Action = reader.GetString(5),
                    ActionTimestamp = reader.GetDateTimeOffset(6),
                    Details = reader.IsDBNull(7) ? null : reader.GetString(7),
                    IPAddress = reader.IsDBNull(8) ? null : reader.GetString(8)
                });
            }

            return list;
        }
    }

}
