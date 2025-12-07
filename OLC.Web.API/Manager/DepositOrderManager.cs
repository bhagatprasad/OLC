using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class DepositOrderManager:IDepositOrderManager
    {
        private readonly string connectionString;
        public DepositOrderManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<DepositOrder>> GetAllDepositOrdersAsync()
        {
            List<DepositOrder> depositOrders = new List<DepositOrder>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("[dbo].[uspGetAllDepositOrders]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

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

        public async Task<List<DepositOrder>> GetDepositOrderByUserIdAsync(long userId)
        {
            List<DepositOrder> depositOrders = new List<DepositOrder>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("[dbo].[uspGetDepositOrdersByUserId]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userId", userId);

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

        public async Task<bool> InsertDepositOrderAsync(DepositOrder depositOrder)
        {
            if(depositOrder == null)
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

        public async Task<List<ExecutiveDepositOrderDetails>> uspGetAllExecutiveDepositOrderDetailsAsync()
        {
            List<ExecutiveDepositOrderDetails> executiveDepositOrderDetailsList = new List<ExecutiveDepositOrderDetails>();
            ExecutiveDepositOrderDetails executiveDepositOrderDetails = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllExecutiveDepositOrderDetails]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter DA= new SqlDataAdapter(sqlCommand);
            
            DataTable dt = new DataTable();
            DA.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count>0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    executiveDepositOrderDetails = new ExecutiveDepositOrderDetails();

                    executiveDepositOrderDetails.UserId = dr["UserId"] != DBNull.Value ? Convert.ToInt64(dr["UserId"]) : null;
                    executiveDepositOrderDetails.DepositOrderId = dr["DepositOrderId"] != DBNull.Value ? Convert.ToInt64(dr["DepositOrderId"]) : null;
                    executiveDepositOrderDetails.PaymentOrderId = dr["PaymentOrderId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentOrderId"]) : null;
                    executiveDepositOrderDetails.PaymentReasonId = dr["PaymentReasonId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentReasonId"]) : null;
                    executiveDepositOrderDetails.DepositeReferance = dr["DepositeReferance"] != DBNull.Value ? dr["DepositeReferance"].ToString() : null;
                    executiveDepositOrderDetails.PaymentReferance = dr["PaymentReferance"] != DBNull.Value ? dr["PaymentReferance"].ToString() : null;
                    executiveDepositOrderDetails.ActualDepositeAmount = dr["ActualDepositeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["ActualDepositeAmount"]) : null;
                    executiveDepositOrderDetails.DepositeAmount = dr["DepositeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["DepositeAmount"]) : null;
                    executiveDepositOrderDetails.PendingDepositeAmount = dr["PendingDepositeAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PendingDepositeAmount"]) : null;
                    executiveDepositOrderDetails.Email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null;
                    executiveDepositOrderDetails.Phone = dr["Phone"] != DBNull.Value ? dr["Phone"].ToString() : null;
                    executiveDepositOrderDetails.CreditCardNumber = dr["CreditCardNumber"] != DBNull.Value ? dr["CreditCardNumber"].ToString() : null;
                    executiveDepositOrderDetails.DepositedTo = dr["DepositedTo"] != DBNull.Value ? dr["DepositedTo"].ToString() : null;
                    executiveDepositOrderDetails.DepositedAccountNumber = dr["DepositedAccountNumber"] != DBNull.Value ? dr["DepositedAccountNumber"].ToString() : null;
                    executiveDepositOrderDetails.IFSCCode = dr["IFSCCode"] != DBNull.Value ? dr["IFSCCode"].ToString() : null;
                    executiveDepositOrderDetails.BankName = dr["BankName"] != DBNull.Value ? dr["BankName"].ToString() : null;
                    executiveDepositOrderDetails.BranchName = dr["BranchName"] != DBNull.Value ? dr["BranchName"].ToString() : null;
                    executiveDepositOrderDetailsList.Add(executiveDepositOrderDetails);
                }
            }
            return executiveDepositOrderDetailsList;
        }
    }
}
