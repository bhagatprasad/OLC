using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class VerifyUserKycManager : IVerifyUserKycManager
    {
        private readonly string connectionString;
        public VerifyUserKycManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<VerifyUserKyc>> GetAllVerifyUserKycAsync()
        {
            List<VerifyUserKyc> verifyUserKycs = new List<VerifyUserKyc>();

            VerifyUserKyc getVerifyUserKyc = null;

            SqlConnection connection=new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand("[dbo].[GetVerifyUserKycProcess]", connection);

            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter=new SqlDataAdapter(command);

            DataTable dt=new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    getVerifyUserKyc =new VerifyUserKyc();

                    getVerifyUserKyc.UserKycId=Convert.ToInt64(item["UserKycId"]);

                    getVerifyUserKyc.UserKycDocumentId = Convert.ToInt64(item["UserKycDocumentId"]);

                    getVerifyUserKyc.Status = item["Status"] != DBNull.Value ? item["Status"].ToString() : null;

                    getVerifyUserKyc.ModifiedBy= item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : (long?)null;

                    getVerifyUserKyc.RejectedReason= item["RejectedReason"] != DBNull.Value ? item["RejectedReason"].ToString() : null;
                }
            }
            return verifyUserKycs;
        }
    }
}
