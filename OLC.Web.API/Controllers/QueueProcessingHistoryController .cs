using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueProcessingHistoryController : ControllerBase
    {
        private readonly IQueueProcessingHistoryManager _historyManager;

        public QueueProcessingHistoryController(
            IQueueProcessingHistoryManager historyManager)
        {
            _historyManager = historyManager;
        }
        public async Task<long> InsertHistoryAsync(QueueProcessingHistory history)
        {
            if (history != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand =
                    new SqlCommand("[dbo].[uspInsertQueueProcessingHistory]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@OrderQueueId", history.OrderQueueId);
                sqlCommand.Parameters.AddWithValue("@ExecutiveId",
                    (object?)history.ExecutiveId ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@FromStatus",
                    (object?)history.FromStatus ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ToStatus", history.ToStatus);
                sqlCommand.Parameters.AddWithValue("@Action", history.Action);
                sqlCommand.Parameters.AddWithValue("@Details",
                    (object?)history.Details ?? DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@IPAddress",
                    (object?)history.IPAddress ?? DBNull.Value);

                long newId = Convert.ToInt64(sqlCommand.ExecuteScalar());

                sqlConnection.Close();
                return newId;
            }

            return 0;
        }
        public async Task<List<QueueProcessingHistory>> GetHistoryByOrderAsync(
    long orderQueueId)
        {
            List<QueueProcessingHistory> historyList =
                new List<QueueProcessingHistory>();

            if (orderQueueId != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand =
                    new SqlCommand("[dbo].[uspGetQueueProcessingHistoryByOrder]",
                                   sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@OrderQueueId", orderQueueId);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    historyList.Add(new QueueProcessingHistory
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        OrderQueueId = Convert.ToInt64(reader["OrderQueueId"]),
                        ExecutiveId = reader["ExecutiveId"] == DBNull.Value
                                        ? null
                                        : Convert.ToInt64(reader["ExecutiveId"]),
                        FromStatus = reader["FromStatus"] == DBNull.Value
                                        ? null
                                        : reader["FromStatus"].ToString(),
                        ToStatus = reader["ToStatus"].ToString(),
                        Action = reader["Action"].ToString(),
                        ActionTimestamp =
                            Convert.ToDateTime(reader["ActionTimestamp"]),
                        Details = reader["Details"] == DBNull.Value
                                        ? null
                                        : reader["Details"].ToString(),
                        IPAddress = reader["IPAddress"] == DBNull.Value
                                        ? null
                                        : reader["IPAddress"].ToString()
                    });
                }

                reader.Close();
                sqlConnection.Close();
            }

            return historyList;
        }


    }
}
