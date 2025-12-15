using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class OrderQueueManager : IOrderQueueManager
    {
        private readonly string connectionString;

        public OrderQueueManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> InsertOrderQueueAsync(long paymentOrderId)
        {
            if (paymentOrderId != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand =
                    new SqlCommand("[dbo].[uspInsertOrderQueue]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@PaymentOrderId", paymentOrderId);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }
}
