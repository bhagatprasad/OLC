using OLC.Web.API.Models;
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

        public async Task<bool> InsertOrderQueueAsync(OrderQueue orderQueue)
        {
            if (orderQueue != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

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

                SqlConnection sqlConnection = new SqlConnection(connectionString);

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

                SqlConnection sqlConnection = new SqlConnection(connectionString);

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

            SqlConnection connection = new SqlConnection(connectionString);

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

        public async Task<List<OrderQueue>> GetPaymentOrderQueueAsync()
        {
            List<OrderQueue> orderQueues = new List<OrderQueue>();

            OrderQueue orderQueue = null;

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetPaymentOrderQueue]", connection);

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
                    orderQueue.UserId = item["UserId"] != DBNull.Value ? Convert.ToInt64(item["UserId"]) : null;
                    orderQueue.PaymentOrderId = Convert.ToInt64(item["PaymentOrderId"]);
                    orderQueue.AssignedExecutiveId = item["AssignedExecutiveId"] != DBNull.Value ? Convert.ToInt64(item["AssignedExecutiveId"]) : null;
                    orderQueue.OrderReference = item["QueueOrderReferance"] != DBNull.Value ? item["QueueOrderReferance"].ToString() : null;
                    orderQueue.OrderReference = item["OrderReference"] != DBNull.Value ? item["OrderReference"].ToString() : null;
                    orderQueue.Amount = item["Amount"] != DBNull.Value ? Convert.ToDecimal(item["Amount"]) : null;
                    orderQueue.UserEmail = item["UserEmail"] != DBNull.Value ? item["UserEmail"].ToString() : null;
                    orderQueue.UserPhone = item["UserPhone"] != DBNull.Value ? item["UserPhone"].ToString() : null;
                    orderQueue.QueueStatus = item["QueueStatus"] != DBNull.Value ? item["QueueStatus"].ToString() : null;
                    orderQueue.TransactionFeeId = item["TransactionFeeId"] != DBNull.Value ? Convert.ToInt64(item["TransactionFeeId"]) : null;
                    orderQueue.Name = item["TransactionFeeName"] != DBNull.Value ? item["TransactionFeeName"].ToString() : null;
                    orderQueue.PlatformFeeAmount = item["PlatformFeeAmount"] != DBNull.Value ? Convert.ToDecimal(item["PlatformFeeAmount"]) : null;
                    orderQueue.FeeCollectionMethod = item["FeeCollectionMethod"] != DBNull.Value ? item["FeeCollectionMethod"].ToString() : null;
                    orderQueue.TotalAmountToChargeCustomer = item["TotalAmountToChargeCustomer"] != DBNull.Value ? Convert.ToDecimal(item["TotalAmountToChargeCustomer"]) : null;
                    orderQueue.TotalAmountToDepositToCustomer = item["TotalAmountToDepositToCustomer"] != DBNull.Value ? Convert.ToDecimal(item["TotalAmountToDepositToCustomer"]) : null;
                    orderQueue.TotalPlatformFee = item["TotalPlatformFee"] != DBNull.Value ? Convert.ToDecimal(item["TotalPlatformFee"]) : null;
                    orderQueue.Currency = item["Currency"] != DBNull.Value ? item["Currency"].ToString() : null;
                    orderQueue.PaymentReasonId = item["PaymentReasonId"] != DBNull.Value ? Convert.ToInt64(item["PaymentReasonId"]) : null;
                    orderQueue.PaymentReasonName = item["PaymentReasonName"] != DBNull.Value ? item["PaymentReasonName"].ToString() : null;
                    orderQueue.CreditCardId = item["CreditCardId"] != DBNull.Value ? Convert.ToInt64(item["CreditCardId"]) : null;
                    orderQueue.CreditCardNumber = item["CreditCardNumber"] != DBNull.Value ? item["CreditCardNumber"].ToString() : null;
                    orderQueue.BankAccountId = item["BankAccountId"] != DBNull.Value ? Convert.ToInt64(item["BankAccountId"]) : null;
                    orderQueue.BankAccountNumber = item["BankAccountNumber"] != DBNull.Value ? item["BankAccountNumber"].ToString() : null;
                    orderQueue.BillingAddressId = item["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(item["BillingAddressId"]) : null;
                    orderQueue.BillingAddress = item["BillingAddress"] != DBNull.Value ? item["BillingAddress"].ToString() : null;
                    orderQueue.OrderStatusId = item["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(item["OrderStatusId"]) : null;
                    orderQueue.OrderStatus = item["OrderStatus"] != DBNull.Value ? item["OrderStatus"].ToString() : null;
                    orderQueue.PaymentStatusId = item["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(item["PaymentStatusId"]) : null;
                    orderQueue.PaymentStatus = item["PaymentStatus"] != DBNull.Value ? item["PaymentStatus"].ToString() : null;
                    orderQueue.DepositStatusId = item["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(item["DepositStatusId"]) : null;
                    orderQueue.DepositStatus = item["DepositStatus"] != DBNull.Value ? item["DepositStatus"].ToString() : null;
                    orderQueue.StripePaymentIntentId = item["StripePaymentIntentId"] != DBNull.Value ? item["StripePaymentIntentId"].ToString() : null;
                    orderQueue.StripePaymentChargeId = item["StripePaymentChargeId"] != DBNull.Value ? item["StripePaymentChargeId"].ToString() : null;
                    orderQueue.StripeDepositeIntentId = item["StripeDepositeIntentId"] != DBNull.Value ? item["StripeDepositeIntentId"].ToString() : null;
                    orderQueue.StripeDepositeChargeId = item["StripeDepositeChargeId"] != DBNull.Value ? item["StripeDepositeChargeId"].ToString() : null;
                    orderQueue.Priority = item["Priority"] != DBNull.Value ? Convert.ToInt32(item["Priority"]) : 0;
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
