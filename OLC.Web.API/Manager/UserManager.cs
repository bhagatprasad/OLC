using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class UserManager : IUserManager
    {
        private readonly string connectionString;
        public UserManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<UserAccount>> GetUserAccountsAsync()
        {
            List<UserAccount> userAccounts = new List<UserAccount>();

            UserAccount userAccount = null;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserAccounts]", connection))
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
                                userAccount = new UserAccount();
                                userAccount.Id = Convert.ToInt64(item["Id"]);
                                userAccount.FirstName = item["FirstName"] != DBNull.Value ? item["FirstName"].ToString() : null;
                                userAccount.LastName = item["LastName"] != DBNull.Value ? item["LastName"].ToString() : null;
                                userAccount.Email = item["Email"] != DBNull.Value ? item["Email"].ToString() : null;
                                userAccount.Phone = item["Phone"] != DBNull.Value ? item["Phone"].ToString() : null;
                                userAccount.RoleId = item["RoleId"] != DBNull.Value ? Convert.ToInt64(item["RoleId"]) : null;
                                userAccount.LastPasswordChangedOn = item["LastPasswordChangedOn"] != DBNull.Value ? (DateTimeOffset?)item["LastPasswordChangedOn"] : null;
                                userAccount.IsBlocked = item["IsBlocked"] != DBNull.Value ? (bool?)item["IsBlocked"] : null;
                                userAccount.IsApproved = item["IsApproved"] != DBNull.Value ? (bool?)item["IsApproved"] : null;
                                userAccount.ApprovedBy = item["ApprovedBy"] != DBNull.Value ? Convert.ToInt64(item["ApprovedBy"]) : null;
                                userAccount.ApprovedOn = item["ApprovedOn"] != DBNull.Value ? (DateTimeOffset?)item["ApprovedOn"] : null;
                                userAccount.KycStatus = item["KycStatus"] != DBNull.Value ? item["KycStatus"].ToString() : null;
                                userAccount.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                                userAccount.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                                userAccount.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                                userAccount.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                                userAccount.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                                userAccount.IsExternalUser = item["IsExternalUser"] != DBNull.Value ? (bool?)item["IsExternalUser"] : null;
                                userAccounts.Add(userAccount);
                            }
                        }
                    }
                }
            }

            return userAccounts;
        }
    }
}
