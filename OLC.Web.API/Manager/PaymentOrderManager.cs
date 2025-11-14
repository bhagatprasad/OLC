
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace OLC.Web.API.Manager
{
    public class PaymentOrderManager : IPaymentOrderManager
    {
        private readonly string connectionString;
        private readonly IUserBankAccountManager _userBankAccountManager;
        private readonly ICreditCardManager _creditCardManager;
        private readonly IBillingAddressManager _billingAddressManager;
        public PaymentOrderManager(IConfiguration configuration, IUserBankAccountManager userBankAccountManager, ICreditCardManager creditCardManager, IBillingAddressManager billingAddressManager)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _userBankAccountManager = userBankAccountManager;
            _creditCardManager = creditCardManager;
            _billingAddressManager = billingAddressManager;
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
            cmd.Parameters.AddWithValue("@sessionId", processPaymentStatus.ChargeId ?? (object)DBNull.Value);
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

                        responsePaymentOrder.OrderStatus = item["OrderStatus"].ToString();

                        responsePaymentOrder.PaymentStatusId = item["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(item["PaymentStatusId"]) : null;

                        responsePaymentOrder.PaymentStatus = item["PaymentStatus"].ToString();

                        responsePaymentOrder.DepositStatusId = item["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(item["DepositStatusId"]) : null;

                        responsePaymentOrder.DepositStatus = item["DepositStatus"].ToString();

                        responsePaymentOrder.StripePaymentIntentId = item["StripePaymentIntentId"] != DBNull.Value ? (item["StripePaymentIntentId"].ToString()) : null;

                        responsePaymentOrder.StripePaymentChargeId = item["StripePaymentChargeId"] != DBNull.Value ? (item["StripePaymentChargeId"].ToString()) : null;

                        responsePaymentOrder.StripeDepositeIntentId = item["StripeDepositeIntentId"] != DBNull.Value ? (item["StripeDepositeIntentId"].ToString()) : null;

                        responsePaymentOrder.StripeDepositeChargeId = item["StripeDepositeChargeId"] != DBNull.Value ? (item["StripeDepositeChargeId"].ToString()) : null;

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

        public async Task<List<UserPaymentOrder>> GetAllUserPaymentOrdersAsync()
        {
            List<UserPaymentOrder> userPaymentOrders = new List<UserPaymentOrder>();

            UserPaymentOrder Upo = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("uspGetAllUserPaymentOrders", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {

                Upo = new UserPaymentOrder();

                Upo.Id = dr["Id"] != DBNull.Value ? Convert.ToInt64(dr["Id"]) : null;

                Upo.OrderReference = dr["OrderReference"] != DBNull.Value ? dr["OrderReference"].ToString() : null;

                Upo.UserId = dr["UserId"] != DBNull.Value ? Convert.ToInt64(dr["UserId"]) : null;

                Upo.PaymentReasonId = dr["PaymentReasonId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentReasonId"]) : null;

                Upo.PaymentReasonName = dr["PaymentReasonName"] != DBNull.Value ? dr["PaymentReasonName"].ToString() : null;

                Upo.Amount = dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : null;

                Upo.TransactionFeeId = dr["TransactionFeeId"] != DBNull.Value ? Convert.ToInt64(dr["TransactionFeeId"]) : null;

                Upo.TransactionFeeAmount = dr["TransactionFeeAmount"] != DBNull.Value ? dr["TransactionFeeAmount"].ToString() : null;

                Upo.PlatformFeeAmount = dr["PlatformFeeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFeeAmount"]) : null;

                Upo.FeeCollectionMethod = dr["FeeCollectionMethod"] != DBNull.Value ? dr["FeeCollectionMethod"].ToString() : null;

                Upo.TotalAmountToChargeCustomer = dr["TotalAmountToChargeCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToChargeCustomer"]) : null;

                Upo.TotalAmountToDepositToCustomer = dr["TotalAmountToDepositToCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToDepositToCustomer"]) : null;

                Upo.TotalPlatformFee = dr["TotalPlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["TotalPlatformFee"]) : null;

                Upo.Currency = dr["Currency"] != DBNull.Value ? dr["Currency"].ToString() : null;

                Upo.CreditCardId = dr["CreditCardId"] != DBNull.Value ? Convert.ToInt64(dr["CreditCardId"]) : null;

                Upo.CreditCardNumber = dr["CreditCardNumber"] != DBNull.Value ? dr["CreditCardNumber"].ToString() : null;

                Upo.BankAccountId = dr["BankAccountId"] != DBNull.Value ? Convert.ToInt64(dr["BankAccountId"]) : null;

                Upo.BillingAddressId = dr["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(dr["BillingAddressId"]) : null;

                Upo.BillingAddress = dr["BillingAddress"] != DBNull.Value ? dr["BillingAddress"].ToString() : null;

                Upo.OrderStatusId = dr["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(dr["OrderStatusId"]) : null;

                Upo.OrderStatus = Convert.ToString(dr["OrderStatus"]);

                Upo.PaymentStatusId = dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : null;

                Upo.PaymentStatus = Convert.ToString(dr["PaymentStatus"]);

                Upo.DepositStatusId = dr["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(dr["DepositStatusId"]) : null;

                Upo.DepositStatus = Convert.ToString(dr["DepositStatus"]);

                Upo.StripePaymentIntentId = Convert.ToString(dr["StripePaymentIntentId"]);

                Upo.StripePaymentChargeId = Convert.ToString(dr["StripePaymentChargeId"]);

                Upo.StripeDepositeIntentId = Convert.ToString(dr["StripeDepositeIntentId"]);

                Upo.StripeDepositeChargeId = Convert.ToString(dr["StripeDepositeChargeId"]);

                Upo.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;

                Upo.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;

                Upo.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;

                Upo.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;

                Upo.IsActive = dr["IsActive"] != DBNull.Value ? (bool?)dr["IsActive"] : null;

                userPaymentOrders.Add(Upo);

            }
            return userPaymentOrders;
        }

        public async Task<List<ExecutivePaymentOrders>> GetExecutivePaymentOrdersAsync()
        {
            List<ExecutivePaymentOrders> userPaymentOrders = new List<ExecutivePaymentOrders>();

            ExecutivePaymentOrders Upo = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("[dbo].[uspGetExecutivePaymentOrders]", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {

                Upo = new ExecutivePaymentOrders();

                Upo.Id = dr["OrderId"] != DBNull.Value ? Convert.ToInt64(dr["OrderId"]) : null;  // Changed from "Id" to "OrderId"

                Upo.OrderReference = dr["OrderReference"] != DBNull.Value ? dr["OrderReference"].ToString() : null;

                Upo.UserId = dr["UserId"] != DBNull.Value ? Convert.ToInt64(dr["UserId"]) : null;

                Upo.UserEmail = dr["UserEmail"] != DBNull.Value ? dr["UserEmail"].ToString() : null;

                Upo.UserPhone = dr["UserPhone"] != DBNull.Value ? dr["UserPhone"].ToString() : null;

                Upo.PaymentReasonId = dr["PaymentReasonId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentReasonId"]) : null;

                Upo.PaymentReasonName = dr["PaymentReasonName"] != DBNull.Value ? dr["PaymentReasonName"].ToString() : null;

                Upo.Amount = dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : null;

                Upo.TransactionFeeId = dr["TransactionFeeId"] != DBNull.Value ? Convert.ToInt64(dr["TransactionFeeId"]) : null;

                Upo.TransactionFeeAmount = dr["TransactionFeeAmount"] != DBNull.Value ? dr["TransactionFeeAmount"].ToString() : null;

                Upo.PlatformFeeAmount = dr["PlatformFeeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFeeAmount"]) : null;

                Upo.FeeCollectionMethod = dr["FeeCollectionMethod"] != DBNull.Value ? dr["FeeCollectionMethod"].ToString() : null;

                Upo.TotalAmountToChargeCustomer = dr["TotalAmountToChargeCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToChargeCustomer"]) : null;

                Upo.TotalAmountToDepositToCustomer = dr["TotalAmountToDepositToCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToDepositToCustomer"]) : null;

                Upo.TotalPlatformFee = dr["TotalPlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["TotalPlatformFee"]) : null;

                Upo.Currency = dr["Currency"] != DBNull.Value ? dr["Currency"].ToString() : null;

                Upo.CreditCardId = dr["CreditCardId"] != DBNull.Value ? Convert.ToInt64(dr["CreditCardId"]) : null;

                Upo.CreditCardNumber = dr["CreditCardNumber"] != DBNull.Value ? dr["CreditCardNumber"].ToString() : null;

                Upo.BankAccountId = dr["BankAccountId"] != DBNull.Value ? Convert.ToInt64(dr["BankAccountId"]) : null;

                Upo.BankAccountNumber = dr["BankAccountNumber"] != DBNull.Value ? dr["BankAccountNumber"].ToString() : null;  // Added this line

                Upo.BillingAddressId = dr["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(dr["BillingAddressId"]) : null;

                Upo.BillingAddress = dr["BillingAddress"] != DBNull.Value ? dr["BillingAddress"].ToString() : null;

                Upo.OrderStatusId = dr["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(dr["OrderStatusId"]) : null;

                Upo.OrderStatus = dr["OrderStatus"] != DBNull.Value ? dr["OrderStatus"].ToString() : null;  // Added null check

                Upo.PaymentStatusId = dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : null;

                Upo.PaymentStatus = dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null;  // Added null check

                Upo.DepositStatusId = dr["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(dr["DepositStatusId"]) : null;

                Upo.DepositStatus = dr["DepositStatus"] != DBNull.Value ? dr["DepositStatus"].ToString() : null;  // Added null check

                Upo.StripePaymentIntentId = dr["StripePaymentIntentId"] != DBNull.Value ? dr["StripePaymentIntentId"].ToString() : null;  // Added null check

                Upo.StripePaymentChargeId = dr["StripePaymentChargeId"] != DBNull.Value ? dr["StripePaymentChargeId"].ToString() : null;  // Added null check

                Upo.StripeDepositeIntentId = dr["StripeDepositeIntentId"] != DBNull.Value ? dr["StripeDepositeIntentId"].ToString() : null;  // Added null check

                Upo.StripeDepositeChargeId = dr["StripeDepositeChargeId"] != DBNull.Value ? dr["StripeDepositeChargeId"].ToString() : null;  // Added null check

                Upo.DepositeAmount = dr["TotalDepositAmount"] != DBNull.Value ? Convert.ToDecimal(dr["TotalDepositAmount"]) : null;

                Upo.PendingDepositeAmount = dr["PendingDepositAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PendingDepositAmount"]) : null;

                Upo.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;

                Upo.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;

                Upo.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;

                Upo.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;

                Upo.IsActive = dr["IsActive"] != DBNull.Value ? (bool?)dr["IsActive"] : null;

                userPaymentOrders.Add(Upo);

            }
            return userPaymentOrders;
        }

        public async Task<PaymentOrderDetails> GetExecutivePaymentOrderDetailsAsync(long paymentOrderId)
        {
            PaymentOrderDetails paymentOrderDetails = new PaymentOrderDetails();

            ExecutivePaymentOrders Upo = new ExecutivePaymentOrders();

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("[dbo].[uspGetExecutivePaymentOrderDetails]", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@paymentOrderId", paymentOrderId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {

                Upo.Id = dr["OrderId"] != DBNull.Value ? Convert.ToInt64(dr["OrderId"]) : null;  // Changed from "Id" to "OrderId"

                Upo.OrderReference = dr["OrderReference"] != DBNull.Value ? dr["OrderReference"].ToString() : null;

                Upo.UserId = dr["UserId"] != DBNull.Value ? Convert.ToInt64(dr["UserId"]) : null;

                Upo.UserEmail = dr["UserEmail"] != DBNull.Value ? dr["UserEmail"].ToString() : null;

                Upo.UserPhone = dr["UserPhone"] != DBNull.Value ? dr["UserPhone"].ToString() : null;

                Upo.PaymentReasonId = dr["PaymentReasonId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentReasonId"]) : null;

                Upo.PaymentReasonName = dr["PaymentReasonName"] != DBNull.Value ? dr["PaymentReasonName"].ToString() : null;

                Upo.Amount = dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : null;

                Upo.TransactionFeeId = dr["TransactionFeeId"] != DBNull.Value ? Convert.ToInt64(dr["TransactionFeeId"]) : null;

                Upo.TransactionFeeAmount = dr["TransactionFeeAmount"] != DBNull.Value ? dr["TransactionFeeAmount"].ToString() : null;

                Upo.PlatformFeeAmount = dr["PlatformFeeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFeeAmount"]) : null;

                Upo.FeeCollectionMethod = dr["FeeCollectionMethod"] != DBNull.Value ? dr["FeeCollectionMethod"].ToString() : null;

                Upo.TotalAmountToChargeCustomer = dr["TotalAmountToChargeCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToChargeCustomer"]) : null;

                Upo.TotalAmountToDepositToCustomer = dr["TotalAmountToDepositToCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToDepositToCustomer"]) : null;

                Upo.TotalPlatformFee = dr["TotalPlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["TotalPlatformFee"]) : null;

                Upo.Currency = dr["Currency"] != DBNull.Value ? dr["Currency"].ToString() : null;

                Upo.CreditCardId = dr["CreditCardId"] != DBNull.Value ? Convert.ToInt64(dr["CreditCardId"]) : null;

                Upo.CreditCardNumber = dr["CreditCardNumber"] != DBNull.Value ? dr["CreditCardNumber"].ToString() : null;

                Upo.BankAccountId = dr["BankAccountId"] != DBNull.Value ? Convert.ToInt64(dr["BankAccountId"]) : null;

                Upo.BankAccountNumber = dr["BankAccountNumber"] != DBNull.Value ? dr["BankAccountNumber"].ToString() : null;  // Added this line

                Upo.BillingAddressId = dr["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(dr["BillingAddressId"]) : null;

                Upo.BillingAddress = dr["BillingAddress"] != DBNull.Value ? dr["BillingAddress"].ToString() : null;

                Upo.OrderStatusId = dr["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(dr["OrderStatusId"]) : null;

                Upo.OrderStatus = dr["OrderStatus"] != DBNull.Value ? dr["OrderStatus"].ToString() : null;  // Added null check

                Upo.PaymentStatusId = dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : null;

                Upo.PaymentStatus = dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null;  // Added null check

                Upo.DepositStatusId = dr["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(dr["DepositStatusId"]) : null;

                Upo.DepositStatus = dr["DepositStatus"] != DBNull.Value ? dr["DepositStatus"].ToString() : null;  // Added null check

                Upo.StripePaymentIntentId = dr["StripePaymentIntentId"] != DBNull.Value ? dr["StripePaymentIntentId"].ToString() : null;  // Added null check

                Upo.StripePaymentChargeId = dr["StripePaymentChargeId"] != DBNull.Value ? dr["StripePaymentChargeId"].ToString() : null;  // Added null check

                Upo.StripeDepositeIntentId = dr["StripeDepositeIntentId"] != DBNull.Value ? dr["StripeDepositeIntentId"].ToString() : null;  // Added null check

                Upo.StripeDepositeChargeId = dr["StripeDepositeChargeId"] != DBNull.Value ? dr["StripeDepositeChargeId"].ToString() : null;  // Added null check

                Upo.DepositeAmount = dr["TotalDepositeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["TotalDepositeAmount"]) : null;

                Upo.PendingDepositeAmount = dr["PendingDepositeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PendingDepositeAmount"]) : null;

                Upo.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;

                Upo.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;

                Upo.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;

                Upo.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;

                Upo.IsActive = dr["IsActive"] != DBNull.Value ? (bool?)dr["IsActive"] : null;

            }

            paymentOrderDetails.paymentOrder = Upo;

            if (Upo != null && Upo.BankAccountId.HasValue)
            {
                var userBankAccount = await _userBankAccountManager.GetUserBankAccountByIdAsync(Upo.BankAccountId.Value);

                if (userBankAccount != null)
                {
                    paymentOrderDetails.paymentOrderBankAccount = userBankAccount;
                }
            }

            if (Upo != null && Upo.CreditCardId.HasValue)
            {
                var userCreditCard = await _creditCardManager.GetUserCreditCardByCardIdAsync(Upo.CreditCardId.Value);

                if (userCreditCard != null)
                {
                    paymentOrderDetails.userCreditCard = userCreditCard;
                }
            }
            if (Upo != null && Upo.BillingAddressId.HasValue)
            {
                var userBillingAddress = await _billingAddressManager.GetUserBillingAddressByIdAsync(Upo.BillingAddressId.Value);
                if (userBillingAddress != null)
                {
                    paymentOrderDetails.userBillingAddress = userBillingAddress;
                }
            }
            //history 

            if (paymentOrderId > 0)
            {
                var paymentOrderHistory = await GetPaymentOrderHistoryAsync(paymentOrderId);

                if (paymentOrderHistory.Any())
                {
                    paymentOrderDetails.paymentOrderHistory = paymentOrderHistory;
                }
            }
            if (paymentOrderId > 0)
            {
                var depositeOrder = await GetDepositOrderByOrderIdAsync(paymentOrderId);
                if (depositeOrder.Any())
                {
                    paymentOrderDetails.DepositeOrders = depositeOrder;
                }
            }

            return paymentOrderDetails;
        }

        public async Task<List<ExecutivePaymentOrders>> GetExecutivePaymentOrderDetailsFilterAsync(PaymentOrderDetailsFilter paymentOrderDetailsFilter)
        {
            List<ExecutivePaymentOrders> userPaymentOrders = new List<ExecutivePaymentOrders>();

            ExecutivePaymentOrders Upo = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("[dbo].[uspGetExecutivePaymentOrdersFilter]", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fromDate", paymentOrderDetailsFilter.FromDate);

            cmd.Parameters.AddWithValue("@toDate", paymentOrderDetailsFilter.ToDate);

            cmd.Parameters.AddWithValue("@orderStatusId", paymentOrderDetailsFilter.OrderStatusId);

            cmd.Parameters.AddWithValue("@paymentStatusId", paymentOrderDetailsFilter.PaymentStatusId);

            cmd.Parameters.AddWithValue("@depositStatusId", paymentOrderDetailsFilter.DepositStatusId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {

                Upo = new ExecutivePaymentOrders();

                Upo.Id = dr["OrderId"] != DBNull.Value ? Convert.ToInt64(dr["OrderId"]) : null;  // Changed from "Id" to "OrderId"

                Upo.OrderReference = dr["OrderReference"] != DBNull.Value ? dr["OrderReference"].ToString() : null;

                Upo.UserId = dr["UserId"] != DBNull.Value ? Convert.ToInt64(dr["UserId"]) : null;

                Upo.UserEmail = dr["UserEmail"] != DBNull.Value ? dr["UserEmail"].ToString() : null;

                Upo.UserPhone = dr["UserPhone"] != DBNull.Value ? dr["UserPhone"].ToString() : null;

                Upo.PaymentReasonId = dr["PaymentReasonId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentReasonId"]) : null;

                Upo.PaymentReasonName = dr["PaymentReasonName"] != DBNull.Value ? dr["PaymentReasonName"].ToString() : null;

                Upo.Amount = dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : null;

                Upo.TransactionFeeId = dr["TransactionFeeId"] != DBNull.Value ? Convert.ToInt64(dr["TransactionFeeId"]) : null;

                Upo.TransactionFeeAmount = dr["TransactionFeeAmount"] != DBNull.Value ? dr["TransactionFeeAmount"].ToString() : null;

                Upo.PlatformFeeAmount = dr["PlatformFeeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFeeAmount"]) : null;

                Upo.FeeCollectionMethod = dr["FeeCollectionMethod"] != DBNull.Value ? dr["FeeCollectionMethod"].ToString() : null;

                Upo.TotalAmountToChargeCustomer = dr["TotalAmountToChargeCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToChargeCustomer"]) : null;

                Upo.TotalAmountToDepositToCustomer = dr["TotalAmountToDepositToCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToDepositToCustomer"]) : null;

                Upo.TotalPlatformFee = dr["TotalPlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["TotalPlatformFee"]) : null;

                Upo.Currency = dr["Currency"] != DBNull.Value ? dr["Currency"].ToString() : null;

                Upo.CreditCardId = dr["CreditCardId"] != DBNull.Value ? Convert.ToInt64(dr["CreditCardId"]) : null;

                Upo.CreditCardNumber = dr["CreditCardNumber"] != DBNull.Value ? dr["CreditCardNumber"].ToString() : null;

                Upo.BankAccountId = dr["BankAccountId"] != DBNull.Value ? Convert.ToInt64(dr["BankAccountId"]) : null;

                Upo.BankAccountNumber = dr["BankAccountNumber"] != DBNull.Value ? dr["BankAccountNumber"].ToString() : null;  // Added this line

                Upo.BillingAddressId = dr["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(dr["BillingAddressId"]) : null;

                Upo.BillingAddress = dr["BillingAddress"] != DBNull.Value ? dr["BillingAddress"].ToString() : null;

                Upo.OrderStatusId = dr["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(dr["OrderStatusId"]) : null;

                Upo.OrderStatus = dr["OrderStatus"] != DBNull.Value ? dr["OrderStatus"].ToString() : null;  // Added null check

                Upo.PaymentStatusId = dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : null;

                Upo.PaymentStatus = dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null;  // Added null check

                Upo.DepositStatusId = dr["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(dr["DepositStatusId"]) : null;

                Upo.DepositStatus = dr["DepositStatus"] != DBNull.Value ? dr["DepositStatus"].ToString() : null;  // Added null check

                Upo.StripePaymentIntentId = dr["StripePaymentIntentId"] != DBNull.Value ? dr["StripePaymentIntentId"].ToString() : null;  // Added null check

                Upo.StripePaymentChargeId = dr["StripePaymentChargeId"] != DBNull.Value ? dr["StripePaymentChargeId"].ToString() : null;  // Added null check

                Upo.StripeDepositeIntentId = dr["StripeDepositeIntentId"] != DBNull.Value ? dr["StripeDepositeIntentId"].ToString() : null;  // Added null check

                Upo.StripeDepositeChargeId = dr["StripeDepositeChargeId"] != DBNull.Value ? dr["StripeDepositeChargeId"].ToString() : null;  // Added null check

                Upo.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;

                Upo.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : null;

                Upo.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;

                Upo.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;

                Upo.IsActive = dr["IsActive"] != DBNull.Value ? (bool?)dr["IsActive"] : null;

                userPaymentOrders.Add(Upo);

            }
            return userPaymentOrders;
        }

        public async Task<ExecutivePaymentOrderSum> GetAllExecutivePaymentOrderSumAsync(PaymentOrderDetailsFilter paymentOrderDetailsFilter)
        {


            ExecutivePaymentOrderSum Upo = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("[dbo].[uspGetAllExecutivePaymentOrderSum]", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fromDate", paymentOrderDetailsFilter.FromDate);

            cmd.Parameters.AddWithValue("@toDate", paymentOrderDetailsFilter.ToDate);

            cmd.Parameters.AddWithValue("@orderStatusId", paymentOrderDetailsFilter.OrderStatusId);

            cmd.Parameters.AddWithValue("@paymentStatusId", paymentOrderDetailsFilter.PaymentStatusId);

            cmd.Parameters.AddWithValue("@depositStatusId", paymentOrderDetailsFilter.DepositStatusId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Upo = new ExecutivePaymentOrderSum();



                Upo.TotalAmount = dr["TotalAmount"] != DBNull.Value ? Convert.ToInt64(dr["TotalAmount"]) : null;

                Upo.DepositedAmount = dr["DepositedAmount"] != DBNull.Value ? Convert.ToInt64(dr["DepositedAmount"]) : null;

                Upo.PlatformFee = dr["PlatformFee"] != DBNull.Value ? Convert.ToInt64(dr["PlatformFee"]) : null;

                Upo.CancelledAmount = dr["SuccessAmount"] != DBNull.Value ? Convert.ToInt64(dr["SuccessAmount"]) : null;

                Upo.FailedAmount = dr["FailedAmount"] != DBNull.Value ? Convert.ToInt64(dr["FailedAmount"]) : null;

                Upo.CancelledAmount = dr["CancelledAmount"] != DBNull.Value ? Convert.ToInt64(dr["CancelledAmount"]) : null;

            }
            return Upo;

        }

        public async Task<bool> InsertDepositOrderAsync(DepositOrder depositOrder)
        {
            if (depositOrder == null)
                return false;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertDepositOrder]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@PaymentOrderId", depositOrder.PaymentOrderId);
            sqlCommand.Parameters.AddWithValue("@OrderReference", depositOrder.OrderReference);
            sqlCommand.Parameters.AddWithValue("@DepositAmount", depositOrder.DepositeAmount);
            sqlCommand.Parameters.AddWithValue("@ActualDepositAmount", depositOrder.ActualDepositeAmount);
            sqlCommand.Parameters.AddWithValue("@PendingDepositAmount", depositOrder.PendingDepositeAmount);
            sqlCommand.Parameters.AddWithValue("@StripeDepositIntentId", depositOrder.StripeDepositeIntentId);
            sqlCommand.Parameters.AddWithValue("@StripeDepositChargeId", depositOrder.StripeDepositeChargeId);
            sqlCommand.Parameters.AddWithValue("@IsPartialPayment", depositOrder.IsPartialPayment);
            sqlCommand.Parameters.AddWithValue("@CreatedBy", depositOrder.CreatedBy);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return true;
        }
        public async Task<List<DepositOrder>> GetDepositOrderByOrderIdAsync(long paymentOrderId)
        {
            List<DepositOrder> depositOrders = new List<DepositOrder>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("[dbo].[uspGetDepositOrdersByOrderId]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PaymentOrderId", paymentOrderId);

                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlDataAdapter.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    DepositOrder responseDepositOrder = new DepositOrder();

                                    responseDepositOrder.Id = Convert.ToInt64(item["Id"]);
                                    responseDepositOrder.PaymentOrderId = Convert.ToInt64(item["PaymentOrderId"]);
                                    responseDepositOrder.OrderReference = item["OrderReference"].ToString();

                                    // Corrected: Map to separate properties
                                    responseDepositOrder.DepositeAmount = Convert.ToDecimal(item["DepositeAmount"]);
                                    responseDepositOrder.ActualDepositeAmount = Convert.ToDecimal(item["ActualDepositeAmount"]);
                                    responseDepositOrder.PendingDepositeAmount = Convert.ToDecimal(item["PendingDepositeAmount"]);

                                    responseDepositOrder.StripeDepositeIntentId = item["StripeDepositeIntentId"] != DBNull.Value ? item["StripeDepositeIntentId"].ToString() : null;
                                    responseDepositOrder.StripeDepositeChargeId = item["StripeDepositeChargeId"] != DBNull.Value ? item["StripeDepositeChargeId"].ToString() : null;

                                    responseDepositOrder.IsPartialPayment = Convert.ToInt64(item["IsPartialPayment"]);
                                    responseDepositOrder.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                                    responseDepositOrder.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                                    responseDepositOrder.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                                    responseDepositOrder.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                                    responseDepositOrder.IsActive = item["IsActive"] != DBNull.Value ? Convert.ToBoolean(item["IsActive"]) : null;

                                    depositOrders.Add(responseDepositOrder);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving deposit orders: {ex.Message}");
                throw;
            }

            return depositOrders;
        }

        public async Task<ExecutivePaymentOrders> GetPaymentOrderDetailsAsync(long paymentOrderId)
        {
            ExecutivePaymentOrders Upo = new ExecutivePaymentOrders();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("[dbo].[GetPaymentOrderDetails]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@paymentOrderId", paymentOrderId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow dr in dt.Rows)
                            {
                                Upo.Id = dr["OrderId"] != DBNull.Value ? Convert.ToInt64(dr["OrderId"]) : (long?)null;
                                Upo.OrderReference = dr["OrderReference"] != DBNull.Value ? dr["OrderReference"].ToString() : null;
                                Upo.UserId = dr["UserId"] != DBNull.Value ? Convert.ToInt64(dr["UserId"]) : (long?)null;
                                Upo.UserEmail = dr["UserEmail"] != DBNull.Value ? dr["UserEmail"].ToString() : null;
                                Upo.UserPhone = dr["UserPhone"] != DBNull.Value ? dr["UserPhone"].ToString() : null;
                                Upo.PaymentReasonId = dr["PaymentReasonId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentReasonId"]) : (long?)null;
                                Upo.PaymentReasonName = dr["PaymentReasonName"] != DBNull.Value ? dr["PaymentReasonName"].ToString() : null;
                                Upo.Amount = dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : (decimal?)null;
                                Upo.TransactionFeeId = dr["TransactionFeeId"] != DBNull.Value ? Convert.ToInt64(dr["TransactionFeeId"]) : (long?)null;
                                Upo.TransactionFeeAmount = dr["TransactionFeeAmount"] != DBNull.Value ? dr["TransactionFeeAmount"].ToString() : null;
                                Upo.PlatformFeeAmount = dr["PlatformFeeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PlatformFeeAmount"]) : (decimal?)null;
                                Upo.FeeCollectionMethod = dr["FeeCollectionMethod"] != DBNull.Value ? dr["FeeCollectionMethod"].ToString() : null;
                                Upo.TotalAmountToChargeCustomer = dr["TotalAmountToChargeCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToChargeCustomer"]) : (decimal?)null;
                                Upo.TotalAmountToDepositToCustomer = dr["TotalAmountToDepositToCustomer"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmountToDepositToCustomer"]) : (decimal?)null;
                                Upo.TotalPlatformFee = dr["TotalPlatformFee"] != DBNull.Value ? Convert.ToDecimal(dr["TotalPlatformFee"]) : (decimal?)null;
                                Upo.Currency = dr["Currency"] != DBNull.Value ? dr["Currency"].ToString() : null;
                                Upo.CreditCardId = dr["CreditCardId"] != DBNull.Value ? Convert.ToInt64(dr["CreditCardId"]) : (long?)null;
                                Upo.CreditCardNumber = dr["CreditCardNumber"] != DBNull.Value ? dr["CreditCardNumber"].ToString() : null;
                                Upo.BankAccountId = dr["BankAccountId"] != DBNull.Value ? Convert.ToInt64(dr["BankAccountId"]) : (long?)null;
                                Upo.BankAccountNumber = dr["BankAccountNumber"] != DBNull.Value ? dr["BankAccountNumber"].ToString() : null;
                                Upo.BillingAddressId = dr["BillingAddressId"] != DBNull.Value ? Convert.ToInt64(dr["BillingAddressId"]) : (long?)null;
                                Upo.BillingAddress = dr["BillingAddress"] != DBNull.Value ? dr["BillingAddress"].ToString() : null;
                                Upo.OrderStatusId = dr["OrderStatusId"] != DBNull.Value ? Convert.ToInt64(dr["OrderStatusId"]) : (long?)null;
                                Upo.OrderStatus = dr["OrderStatus"] != DBNull.Value ? dr["OrderStatus"].ToString() : null;
                                Upo.PaymentStatusId = dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : (long?)null;
                                Upo.PaymentStatus = dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null;
                                Upo.DepositStatusId = dr["DepositStatusId"] != DBNull.Value ? Convert.ToInt64(dr["DepositStatusId"]) : (long?)null;
                                Upo.DepositStatus = dr["DepositStatus"] != DBNull.Value ? dr["DepositStatus"].ToString() : null;
                                Upo.StripePaymentIntentId = dr["StripePaymentIntentId"] != DBNull.Value ? dr["StripePaymentIntentId"].ToString() : null;
                                Upo.StripePaymentChargeId = dr["StripePaymentChargeId"] != DBNull.Value ? dr["StripePaymentChargeId"].ToString() : null;
                                Upo.StripeDepositeIntentId = dr["StripeDepositIntentId"] != DBNull.Value ? dr["StripeDepositIntentId"].ToString() : null;
                                Upo.StripeDepositeChargeId = dr["StripeDepositChargeId"] != DBNull.Value ? dr["StripeDepositChargeId"].ToString() : null;
                                Upo.DepositeAmount = dr["TotalDepositAmount"] != DBNull.Value ? Convert.ToDecimal(dr["TotalDepositAmount"]) : (decimal?)null;  // Added mapping
                                Upo.PendingDepositeAmount = dr["PendingDepositAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PendingDepositAmount"]) : (decimal?)null;  // Added mapping
                                Upo.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : (long?)null;
                                Upo.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset)dr["CreatedOn"] : (DateTimeOffset?)null;
                                Upo.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : (long?)null;
                                Upo.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : (DateTimeOffset?)null;
                                Upo.IsActive = dr["IsActive"] != DBNull.Value ? (bool?)dr["IsActive"] : null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed (e.g., throw or return null)
                    throw new Exception("Error retrieving payment order details: " + ex.Message);
                }
            }

            return Upo;
        }

    }
}