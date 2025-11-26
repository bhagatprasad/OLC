using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class PaymentReasonManager : IPaymentReasonManager
    {
        private readonly string connectionString;
        public PaymentReasonManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<PaymentReason>> GetPaymentReasonsAsync()
        {
            List<PaymentReason> paymentReasons = new List<PaymentReason>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetPaymentReasons]", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dt = new DataTable();
                        sqlDataAdapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                PaymentReason paymentReason = new PaymentReason();

                                paymentReason.Id = Convert.ToInt64(item["Id"]);
                                paymentReason.Name = item["Name"] != DBNull.Value ? item["Name"].ToString() : null;
                                paymentReason.Description = item["Description"] != DBNull.Value ? item["Description"].ToString() : null;
                                paymentReason.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                                paymentReason.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                                paymentReason.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                                paymentReason.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                                paymentReason.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                                paymentReasons.Add(paymentReason);
                            }
                        }
                    }
                }
            }

            return paymentReasons;
        }
    }
}
