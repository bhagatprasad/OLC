
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class PaymentOrderManager : IPaymentOrderManager
    {
        private readonly string connectionString;
        public PaymentOrderManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<PaymentOrder> InsertPaymentOrderAsync(PaymentOrder paymentOrder)
        {
            PaymentOrder responsePaymentOrder = new PaymentOrder();

            if (paymentOrder != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertPaymentOrder]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@orderReference", paymentOrder.OrderReference);

                sqlCommand.Parameters.AddWithValue("@userId", paymentOrder.UserId);

                sqlCommand.Parameters.AddWithValue("@paymentReasonId", paymentOrder.PaymentReasonId);

                sqlCommand.Parameters.AddWithValue("@amount", paymentOrder.Amount);

                sqlCommand.Parameters.AddWithValue("@transactionFeeId", paymentOrder.TransactionFeeId);

                sqlCommand.Parameters.AddWithValue("@platformFeeAmount", paymentOrder.PlatformFeeAmount);

                sqlCommand.Parameters.AddWithValue("@feeCollectionMethod", paymentOrder.FeeCollectionMethod);

                sqlCommand.Parameters.AddWithValue("@totalAmountToChargeCustomer", paymentOrder.TotalAmountToChargeCustomer);

                sqlCommand.Parameters.AddWithValue("@totalAmountToDepositToCustomer", paymentOrder.TotalAmountToDepositToCustomer);

                sqlCommand.Parameters.AddWithValue("@totalPlatformFee", paymentOrder.TotalPlatformFee);

                sqlCommand.Parameters.AddWithValue("@currency", paymentOrder.Currency);

                sqlCommand.Parameters.AddWithValue("@creditCardId", paymentOrder.CreditCardId);

                sqlCommand.Parameters.AddWithValue("@bankAccountId", paymentOrder.BankAccountId);

                sqlCommand.Parameters.AddWithValue("@billingAddressId", paymentOrder.BillingAddressId);

                sqlCommand.Parameters.AddWithValue("@orderStatusId", paymentOrder.OrderStatusId);

                sqlCommand.Parameters.AddWithValue("@paymentStatusId", paymentOrder.PaymentStatusId);

                sqlCommand.Parameters.AddWithValue("@depositStatusId", paymentOrder.DepositStatusId);

                sqlCommand.Parameters.AddWithValue("@stripePaymentIntentId", paymentOrder.StripePaymentIntentId);

                sqlCommand.Parameters.AddWithValue("@stripePaymentChargeId", paymentOrder.StripePaymentIntentId);

                sqlCommand.Parameters.AddWithValue("@stripeDepositIntentId", paymentOrder.StripeDepositeIntentId);

                sqlCommand.Parameters.AddWithValue("@stripeDepositeChargeId", paymentOrder.StripePaymentChargeId);

                sqlCommand.Parameters.AddWithValue("@createdBy", paymentOrder.CreatedBy);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dt = new DataTable();

                sqlDataAdapter.Fill(dt);

                sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {
                    responsePaymentOrder = PreLoadPaymentOrderAsync(dt.Rows[0]);

                }
            }

            return responsePaymentOrder;
        }

        public async Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId)
        {
            List<PaymentOrder> getPaymentOrders = new List<PaymentOrder>();

            PaymentOrder getPaymentOrder = new PaymentOrder();

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetPayamentOrdersByUserId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@userId", userId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getPaymentOrders.Add(PreLoadPaymentOrderAsync(item));
                }
            }
            return getPaymentOrders;
        }

        public async Task<List<PaymentOrder>> GetPaymentOrdersAsync()
        {
            List<PaymentOrder> getPaymentOrders = new List<PaymentOrder>();

            PaymentOrder getPaymentOrder = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetPaymentOrders]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    getPaymentOrders.Add(PreLoadPaymentOrderAsync(item));
                }
            }
            return getPaymentOrders;
        }

        public async Task<List<PaymentOrderHistory>> GetPaymentOrderHistoryAsync(long paymentOrderId)
        {
            List<PaymentOrderHistory> paymentOrderHistories = new List<PaymentOrderHistory>();

            PaymentOrderHistory paymentOrderHistory = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("uspGetPaymentOrderHistory", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@paymentOrderId", paymentOrderId);

            DataTable dt = new DataTable();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            foreach (DataRow dr in dt.Rows)
            {
                paymentOrderHistory = new PaymentOrderHistory();

                paymentOrderHistory.Id = Convert.ToInt32(dr["Id"]);
                paymentOrderHistory.StatusId = dr["StatusId"] != DBNull.Value ? Convert.ToInt32(dr["StatusId"]) : null;
                paymentOrderHistory.Description = dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null;
                paymentOrderHistory.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt32(dr["CreatedBy"]) : null;
                paymentOrderHistory.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;
                paymentOrderHistory.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                paymentOrderHistory.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(dr["ModifiedBy"]) : null;
                paymentOrderHistory.IsActive = dr["IsActive"] != DBNull.Value ? (bool)dr["IsActive"] : null;
                paymentOrderHistories.Add(paymentOrderHistory);
            }


            return paymentOrderHistories;
        }

        public async Task<PaymentOrder> ProcessPaymentOrderAsync(ProcessPaymentOrder processPaymentOrder)
        {
            PaymentOrder responsePaymentOrder = new PaymentOrder();

            if (processPaymentOrder != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspProcessPaymentOrder]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@paymentOrderId", processPaymentOrder.PaymentOrderId);

                sqlCommand.Parameters.AddWithValue("@orderStatusId", processPaymentOrder.OrderStatusId);

                sqlCommand.Parameters.AddWithValue("@paymentStatusId", processPaymentOrder.PaymentStatusId);

                sqlCommand.Parameters.AddWithValue("@depositeStatusId", processPaymentOrder.DepositeStatusId);

                sqlCommand.Parameters.AddWithValue("@createdBy", processPaymentOrder.CreatedBy);

                sqlCommand.Parameters.AddWithValue("@description", processPaymentOrder.Description);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dt = new DataTable();

                sqlDataAdapter.Fill(dt);

                sqlConnection.Close();

                if (dt.Rows.Count > 0)
                {
                    responsePaymentOrder = PreLoadPaymentOrderAsync(dt.Rows[0]);
                }
            }
            return responsePaymentOrder;
        }

        public async Task<PaymentOrder> ProcessPaymentStatusAsync(ProcessPaymentStatus processPaymentStatus)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[uspProcessPaymentStatus]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters based on the ProcessPaymentStatus object
            cmd.Parameters.AddWithValue("@paymentOrderId", processPaymentStatus.PaymentOrderId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@sessionId", processPaymentStatus.SessionId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@paymentIntentId", processPaymentStatus.PaymentIntentId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@paymentMethod", processPaymentStatus.PaymentMethod ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@orderStatusId", processPaymentStatus.OrderStatusId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@paymentStatusId", processPaymentStatus.PaymentStatusId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@description", processPaymentStatus.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@userId", processPaymentStatus.UserId?.ToString() ?? (object)DBNull.Value); // Assuming UserId is long? and converting to string for varchar(max)

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            conn.Close();

            PaymentOrder paymentOrder = new PaymentOrder();
            ;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0]; 

                paymentOrder = PreLoadPaymentOrderAsync(dr);
            }

            return paymentOrder;
        }


        private PaymentOrder PreLoadPaymentOrderAsync(DataRow item)
        {
            PaymentOrder responsePaymentOrder = new PaymentOrder();

            if (item != null)
            {
                responsePaymentOrder.Id = Convert.ToInt64(item["Id"]);

                responsePaymentOrder.OrderReference = item["OrderReference"].ToString();

                responsePaymentOrder.UserId = Convert.ToInt64(item["UserId"]);

                responsePaymentOrder.PaymentReasonId = Convert.ToInt64(item["PaymentReasonId"]);

                responsePaymentOrder.Amount = Convert.ToDecimal(item["Amount"]);

                responsePaymentOrder.TransactionFeeId = Convert.ToInt64(item["TransactionFeeId"]);

                responsePaymentOrder.PlatformFeeAmount = Convert.ToDecimal(item["PlatformFeeAmount"]);

                responsePaymentOrder.FeeCollectionMethod = item["FeeCollectionMethod"] != DBNull.Value ? item["FeeCollectionMethod"].ToString() : null;

                responsePaymentOrder.TotalAmountToChargeCustomer = Convert.ToDecimal(item["TotalAmountToChargeCustomer"]);

                responsePaymentOrder.TotalAmountToDepositToCustomer = Convert.ToDecimal(item["TotalAmountToDepositToCustomer"]);

                responsePaymentOrder.TotalPlatformFee = Convert.ToDecimal(item["TotalPlatformFee"]);

                responsePaymentOrder.Currency = item["Currency"].ToString();

                responsePaymentOrder.CreditCardId = item["CreditCardId"] != DBNull.Value ? Convert.ToInt64(item["CreditCardId"]) : null;

                responsePaymentOrder.BankAccountId = item["BankAccountId"] != DBNull.Value ? Convert.ToInt64(item["BankAccountId"]) : null;

                responsePaymentOrder.BillingAddressId = item["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(item["BillingAddressId"]) : null;

                responsePaymentOrder.OrderStatusId = item["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(item["OrderStatusId"]) : null;

                responsePaymentOrder.PaymentStatusId = item["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(item["PaymentStatusId"]) : null;

                responsePaymentOrder.DepositStatusId = item["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(item["DepositStatusId"]) : null;

                responsePaymentOrder.StripePaymentIntentId = item["StripePaymentIntentId"] != DBNull.Value ? (item["StripePaymentIntentId"].ToString()) : null;

                responsePaymentOrder.StripePaymentChargeId = item["StripePaymentChargeId"] != DBNull.Value ? (item["StripePaymentChargeId"].ToString()) : null;

                responsePaymentOrder.StripeDepositeIntentId = item["StripeDepositeIntentId"] != DBNull.Value ? (item["StripeDepositeIntentId"].ToString()) : null;

                responsePaymentOrder.StripeDepositeChargeId = item["StripeDepositeChargeId"] != DBNull.Value ? (item["StripeDepositeChargeId"].ToString()) : null;

                responsePaymentOrder.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                responsePaymentOrder.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                responsePaymentOrder.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                responsePaymentOrder.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                responsePaymentOrder.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
            }

            return responsePaymentOrder;
        }

        public async Task<List<UserPaymentOrder>> GetUserPaymentOrderListAsync(long userId)
        {
            List<UserPaymentOrder> userPaymentOrders = new List<UserPaymentOrder>();           

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserPaymentOrders]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@userId", userId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    UserPaymentOrder responsePaymentOrder = new UserPaymentOrder();

                    if (item != null)
                    {
                        responsePaymentOrder.Id = Convert.ToInt64(item["Id"]);

                        responsePaymentOrder.OrderReference = item["OrderReference"].ToString();

                        responsePaymentOrder.UserId = Convert.ToInt64(item["UserId"]);

                        responsePaymentOrder.PaymentReasonId = Convert.ToInt64(item["PaymentReasonId"]);

                        responsePaymentOrder.PaymentReasonName = item["PaymentReasonName"] != DBNull.Value ? item["PaymentReasonName"].ToString() : null;

                        responsePaymentOrder.Amount = Convert.ToDecimal(item["Amount"]);

                        responsePaymentOrder.TransactionFeeId = Convert.ToInt64(item["TransactionFeeId"]);

                        responsePaymentOrder.TransactionFeeAmount = item["TransactionFeeAmount"] != DBNull.Value ? item["TransactionFeeAmount"].ToString() : null;

                        responsePaymentOrder.PlatformFeeAmount = Convert.ToDecimal(item["PlatformFeeAmount"]);

                        responsePaymentOrder.FeeCollectionMethod = item["FeeCollectionMethod"] != DBNull.Value ? item["FeeCollectionMethod"].ToString() : null;

                        responsePaymentOrder.TotalAmountToChargeCustomer = Convert.ToDecimal(item["TotalAmountToChargeCustomer"]);

                        responsePaymentOrder.TotalAmountToDepositToCustomer = Convert.ToDecimal(item["TotalAmountToDepositToCustomer"]);

                        responsePaymentOrder.TotalPlatformFee = Convert.ToDecimal(item["TotalPlatformFee"]);

                        responsePaymentOrder.Currency = item["Currency"].ToString();

                        responsePaymentOrder.CreditCardId = item["CreditCardId"] != DBNull.Value ? Convert.ToInt64(item["CreditCardId"]) : null;

                        responsePaymentOrder.CreditCardNumber = item["CreditCardNumber"] != DBNull.Value ? item["CreditCardNumber"].ToString() : null;

                        responsePaymentOrder.BankAccountId = item["BankAccountId"] != DBNull.Value ? Convert.ToInt64(item["BankAccountId"]) : null;

                        responsePaymentOrder.BankAccountNumber = item["BankAccountNumber"] != DBNull.Value ? item["BankAccountNumber"].ToString() : null;

                        responsePaymentOrder.BillingAddressId = item["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(item["BillingAddressId"]) : null;

                        responsePaymentOrder.BillingAddress = item["BillingAddress"] != DBNull.Value ? item["BillingAddress"].ToString() : null;

                        responsePaymentOrder.OrderStatusId = item["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(item["OrderStatusId"]) : null;

                        responsePaymentOrder.OrderStatus =  item["OrderStatus"].ToString();

                        responsePaymentOrder.PaymentStatusId = item["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(item["PaymentStatusId"]) : null;

                        responsePaymentOrder.PaymentStatus = item["PaymentStatus"].ToString();

                        responsePaymentOrder.DepositStatusId = item["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(item["DepositStatusId"]) : null;

                        responsePaymentOrder.DepositStatus = item["DepositStatus"].ToString();

                        responsePaymentOrder.StripePaymentIntentId = item["StripePaymentIntentId"] != DBNull.Value ? (item["StripePaymentIntentId"].ToString()) : null;

                        responsePaymentOrder.StripePaymentChargeId = item["StripePaymentChargeId"] != DBNull.Value ? (item["StripePaymentChargeId"].ToString()) : null;

                        responsePaymentOrder.StripeDepositIntentId = item["StripeDepositeIntentId"] != DBNull.Value ? (item["StripeDepositeIntentId"].ToString()) : null;

                        responsePaymentOrder.StripeDepositChargeId = item["StripeDepositeChargeId"] != DBNull.Value ? (item["StripeDepositeChargeId"].ToString()) : null;

                        responsePaymentOrder.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                        responsePaymentOrder.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                        responsePaymentOrder.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                        responsePaymentOrder.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                        responsePaymentOrder.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                    }

                    userPaymentOrders.Add(responsePaymentOrder);
                }
            }
            return userPaymentOrders;
        }
    }
}
