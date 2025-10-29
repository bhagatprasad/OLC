
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
                    foreach (DataRow item in dt.Rows)
                    {

                        responsePaymentOrder.Id = Convert.ToInt64(item["Id"]);

                        responsePaymentOrder.OrderReference = item["OrderReference"].ToString();

                        responsePaymentOrder.UserId = Convert.ToInt64(item["UserId"]);

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

                }
            }

            return responsePaymentOrder;
        }

        public async Task<List<PaymentOrder>> GetPaymentOrdersByUserIdAsync(long userId)
        {
            List<PaymentOrder> getPaymentOrders = new List<PaymentOrder>();

            PaymentOrder getPaymentOrderByUserId = null;

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

                    getPaymentOrderByUserId = new PaymentOrder();

                    getPaymentOrderByUserId.Id = Convert.ToInt64(item["Id"]);

                    getPaymentOrderByUserId.OrderReference = item["OrderReference"].ToString();

                    getPaymentOrderByUserId.UserId = Convert.ToInt64(item["UserId"]);

                    getPaymentOrderByUserId.Amount = Convert.ToDecimal(item["Amount"]);

                    getPaymentOrderByUserId.TransactionFeeId = Convert.ToInt64(item["TransactionFeeId"]);

                    getPaymentOrderByUserId.PlatformFeeAmount = Convert.ToDecimal(item["PlatformFeeAmount"]);

                    getPaymentOrderByUserId.FeeCollectionMethod = item["FeeCollectionMethod"] != DBNull.Value ? item["FeeCollectionMethod"].ToString() : null;

                    getPaymentOrderByUserId.TotalAmountToChargeCustomer = Convert.ToDecimal(item["TotalAmountToChargeCustomer"]);

                    getPaymentOrderByUserId.TotalAmountToDepositToCustomer = Convert.ToDecimal(item["TotalAmountToDepositToCustomer"]);

                    getPaymentOrderByUserId.TotalPlatformFee = Convert.ToDecimal(item["TotalPlatformFee"]);

                    getPaymentOrderByUserId.Currency = item["Currency"].ToString();

                    getPaymentOrderByUserId.CreditCardId = item["CreditCardId"] != DBNull.Value ? Convert.ToInt64(item["CreditCardId"]) : null;

                    getPaymentOrderByUserId.BankAccountId = item["BankAccountId"] != DBNull.Value ? Convert.ToInt64(item["BankAccountId"]) : null;

                    getPaymentOrderByUserId.BillingAddressId = item["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(item["BillingAddressId"]) : null;

                    getPaymentOrderByUserId.OrderStatusId = item["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(item["OrderStatusId"]) : null;

                    getPaymentOrderByUserId.PaymentStatusId = item["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(item["PaymentStatusId"]) : null;

                    getPaymentOrderByUserId.DepositStatusId = item["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(item["DepositStatusId"]) : null;

                    getPaymentOrderByUserId.StripePaymentIntentId = item["StripePaymentIntentId"] != DBNull.Value ? (item["StripePaymentIntentId"].ToString()) : null;

                    getPaymentOrderByUserId.StripePaymentChargeId = item["StripePaymentChargeId"] != DBNull.Value ? (item["StripePaymentChargeId"].ToString()) : null;

                    getPaymentOrderByUserId.StripeDepositeIntentId = item["StripeDepositeIntentId"] != DBNull.Value ? (item["StripeDepositeIntentId"].ToString()) : null;

                    getPaymentOrderByUserId.StripeDepositeChargeId = item["StripeDepositeChargeId"] != DBNull.Value ? (item["StripeDepositeChargeId"].ToString()) : null;

                    getPaymentOrderByUserId.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getPaymentOrderByUserId.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getPaymentOrderByUserId.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    getPaymentOrderByUserId.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getPaymentOrderByUserId.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getPaymentOrders.Add(getPaymentOrderByUserId);
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

                    getPaymentOrder = new PaymentOrder();

                    getPaymentOrder.Id = Convert.ToInt64(item["Id"]);

                    getPaymentOrder.OrderReference = item["OrderReference"].ToString();

                    getPaymentOrder.UserId = Convert.ToInt64(item["UserId"]);

                    getPaymentOrder.Amount = Convert.ToDecimal(item["Amount"]);

                    getPaymentOrder.TransactionFeeId = Convert.ToInt64(item["TransactionFeeId"]);

                    getPaymentOrder.PlatformFeeAmount = Convert.ToDecimal(item["PlatformFeeAmount"]);

                    getPaymentOrder.FeeCollectionMethod = item["FeeCollectionMethod"] != DBNull.Value ? item["FeeCollectionMethod"].ToString() : null;

                    getPaymentOrder.TotalAmountToChargeCustomer = Convert.ToDecimal(item["TotalAmountToChargeCustomer"]);

                    getPaymentOrder.TotalAmountToDepositToCustomer = Convert.ToDecimal(item["TotalAmountToDepositToCustomer"]);

                    getPaymentOrder.TotalPlatformFee = Convert.ToDecimal(item["TotalPlatformFee"]);

                    getPaymentOrder.Currency = item["Currency"].ToString();

                    getPaymentOrder.CreditCardId = item["CreditCardId"] != DBNull.Value ? Convert.ToInt64(item["CreditCardId"]) : null;

                    getPaymentOrder.BankAccountId = item["BankAccountId"] != DBNull.Value ? Convert.ToInt64(item["BankAccountId"]) : null;

                    getPaymentOrder.BillingAddressId = item["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(item["BillingAddressId"]) : null;

                    getPaymentOrder.OrderStatusId = item["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(item["OrderStatusId"]) : null;

                    getPaymentOrder.PaymentStatusId = item["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(item["PaymentStatusId"]) : null;

                    getPaymentOrder.DepositStatusId = item["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(item["DepositStatusId"]) : null;

                    getPaymentOrder.StripePaymentIntentId = item["StripePaymentIntentId"] != DBNull.Value ? (item["StripePaymentIntentId"].ToString()) : null;

                    getPaymentOrder.StripePaymentChargeId = item["StripePaymentChargeId"] != DBNull.Value ? (item["StripePaymentChargeId"].ToString()) : null;

                    getPaymentOrder.StripeDepositeIntentId = item["StripeDepositeIntentId"] != DBNull.Value ? (item["StripeDepositeIntentId"].ToString()) : null;

                    getPaymentOrder.StripeDepositeChargeId = item["StripeDepositeChargeId"] != DBNull.Value ? (item["StripeDepositeChargeId"].ToString()) : null;

                    getPaymentOrder.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getPaymentOrder.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getPaymentOrder.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    getPaymentOrder.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getPaymentOrder.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getPaymentOrders.Add(getPaymentOrder);
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
                    foreach (DataRow item in dt.Rows)
                    {

                        responsePaymentOrder.Id = Convert.ToInt64(item["Id"]);

                        responsePaymentOrder.OrderReference = item["OrderReference"].ToString();

                        responsePaymentOrder.UserId = Convert.ToInt64(item["UserId"]);

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
                }                
            }
            return responsePaymentOrder;
        }
    }
}
