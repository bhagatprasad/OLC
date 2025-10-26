
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

        public async  Task<bool> InsertPaymentOrderAsync(PaymentOrder paymentOrder)
        {
           if (paymentOrder != null)
           {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertPaymentOrder]",sqlConnection);

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

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
           }
            return false;                       
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

                    getPaymentOrderByUserId.Amount = Convert.ToInt64(item["Amount"]);

                    getPaymentOrderByUserId.TransactionFeeId = Convert.ToInt64(item["TransactionFeeId"]) ;

                    getPaymentOrderByUserId.PlatformFeeAmount =  Convert.ToInt64(item["PlatformFeeAmount"]);

                    getPaymentOrderByUserId.FeeCollectionMethod = item["FeeCollectionMethod"] != DBNull.Value ? item["FeeCollectionMethod"].ToString() : null;

                    getPaymentOrderByUserId.TotalAmountToChargeCustomer =  Convert.ToInt64(item["TotalAmountToChargeCustomer"]);

                    getPaymentOrderByUserId.TotalAmountToDepositToCustomer =  Convert.ToInt64(item["TotalAmountToDepositToCustomer"]);

                    getPaymentOrderByUserId.TotalPlatformFee = Convert.ToInt64(item["TotalPlatformFee"]);

                    getPaymentOrderByUserId.Currency = item["Currency"].ToString();

                    getPaymentOrderByUserId.CreditCardId = item["CreditCardId"] != DBNull.Value ? Convert.ToInt64(item["CreditCardId"]) : null;

                    getPaymentOrderByUserId.BankAccountId = item["BankAccountId"] != DBNull.Value ? Convert.ToInt64(item["BankAccountId"]) : null;

                    getPaymentOrderByUserId.BillingAddressId = item["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(item["BillingAddressId"]) : null;

                    getPaymentOrderByUserId.OrderStatusId = item["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(item["OrderStatusId"]) : null;

                    getPaymentOrderByUserId.PaymentStatusId = item["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(item["PaymentStatusId"]):null;

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

                    getPaymentOrder.Amount = Convert.ToInt64(item["Amount"]);

                    getPaymentOrder.TransactionFeeId = Convert.ToInt64(item["TransactionFeeId"]);

                    getPaymentOrder.PlatformFeeAmount = Convert.ToInt64(item["PlatformFeeAmount"]);

                    getPaymentOrder.FeeCollectionMethod = item["FeeCollectionMethod"] != DBNull.Value ? item["FeeCollectionMethod"].ToString() : null;

                    getPaymentOrder.TotalAmountToChargeCustomer = Convert.ToInt64(item["TotalAmountToChargeCustomer"]);

                    getPaymentOrder.TotalAmountToDepositToCustomer = Convert.ToInt64(item["TotalAmountToDepositToCustomer"]);

                    getPaymentOrder.TotalPlatformFee = Convert.ToInt64(item["TotalPlatformFee"]);

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
    }
}
