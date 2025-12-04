using OLC.Web.API.Models;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Data;

namespace OLC.Web.API.Manager
{
    public class UserLoginHistoryManager : IUserLoginHistoryManager
    {
        private readonly string connectionString;

        public UserLoginHistoryManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<List<UserLoginHistory>> GetAllUserActivityTodayAsync()
        {
            List<UserLoginHistory> userLoginHistories = new List<UserLoginHistory>();
            UserLoginHistory userLoginHistory = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllUserActivityToday]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    userLoginHistory = new UserLoginHistory();

                    userLoginHistory.Id = Convert.ToInt64(dr["Id"]);
                    userLoginHistory.UserId = dr["UserId"] != DBNull.Value ? (long?)Convert.ToInt64(dr["UserId"]) : null;
                    userLoginHistory.EmailAttempted = dr["EmailAttempted"] != DBNull.Value ? dr["EmailAttempted"].ToString() : null;
                    userLoginHistory.LoginAttemptTime = (DateTimeOffset)dr["LoginAttemptTime"];
                    userLoginHistory.IsSuccess = dr["IsSuccess"] != DBNull.Value ? Convert.ToBoolean(dr["IsSuccess"]) : false;
                    userLoginHistory.StatusCode = dr["StatusCode"] != DBNull.Value ? Convert.ToBoolean(dr["StatusCode"]) : false;
                    userLoginHistory.StatusMessage = dr["StatusMessage"] != DBNull.Value ? dr["StatusMessage"].ToString() : null;
                    userLoginHistory.Notes = dr["Notes"] != DBNull.Value ? dr["Notes"].ToString() : null;
                    userLoginHistory.Problem = dr["Problem"] != DBNull.Value ? dr["Problem"].ToString() : null;
                    userLoginHistory.LoggedInFrom = dr["LoggedInFrom"] != DBNull.Value ? dr["LoggedInFrom"].ToString() : null;
                    userLoginHistory.IpAddress = dr["IpAddress"] != DBNull.Value ? dr["IpAddress"].ToString() : null;
                    userLoginHistory.UserAgent = dr["UserAgent"] != DBNull.Value ? dr["UserAgent"].ToString() : null;
                    userLoginHistory.FailedReason = dr["FailedReason"] != DBNull.Value ? dr["FailedReason"].ToString() : null;
                    userLoginHistory.ConsecutiveFailures = Convert.ToInt32(dr["ConsecutiveFailures"]);
                    userLoginHistory.TotalFailures15Min = Convert.ToInt32(dr["TotalFailures15Min"]);
                    userLoginHistory.WasBlocked = Convert.ToBoolean(dr["WasBlocked"]);
                    userLoginHistory.CreatedOn = (DateTimeOffset)dr["CreatedOn"];
                    userLoginHistories.Add(userLoginHistory);
                }
            }
            return userLoginHistories;
        }

        public async Task<List<UserLoginHistory>> GetAllUserLoginHistoryAsync()
        {
            List<UserLoginHistory> userLoginHistories = new List<UserLoginHistory>();
            UserLoginHistory userLoginHistory = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllUserLoginHistory]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    userLoginHistory = new UserLoginHistory();

                    userLoginHistory.Id = Convert.ToInt64(dr["Id"]);
                    userLoginHistory.UserId = dr["UserId"] != DBNull.Value ? (long?)Convert.ToInt64(dr["UserId"]) : null;
                    userLoginHistory.EmailAttempted = dr["EmailAttempted"] != DBNull.Value ? dr["EmailAttempted"].ToString() : null;
                    userLoginHistory.LoginAttemptTime = (DateTimeOffset)dr["LoginAttemptTime"];
                    userLoginHistory.IsSuccess = dr["IsSuccess"] != DBNull.Value ? Convert.ToBoolean(dr["IsSuccess"]) : false;
                    userLoginHistory.StatusCode = dr["StatusCode"] != DBNull.Value ? Convert.ToBoolean(dr["StatusCode"]) : false;
                    userLoginHistory.StatusMessage = dr["StatusMessage"] != DBNull.Value ? dr["StatusMessage"].ToString() : null;
                    userLoginHistory.Notes = dr["Notes"] != DBNull.Value ? dr["Notes"].ToString() : null;
                    userLoginHistory.Problem = dr["Problem"] != DBNull.Value ? dr["Problem"].ToString() : null;
                    userLoginHistory.LoggedInFrom = dr["LoggedInFrom"] != DBNull.Value ? dr["LoggedInFrom"].ToString() : null;
                    userLoginHistory.IpAddress = dr["IpAddress"] != DBNull.Value ? dr["IpAddress"].ToString() : null;
                    userLoginHistory.UserAgent = dr["UserAgent"] != DBNull.Value ? dr["UserAgent"].ToString() : null;
                    userLoginHistory.FailedReason = dr["FailedReason"] != DBNull.Value ? dr["FailedReason"].ToString() : null;
                    userLoginHistory.ConsecutiveFailures = Convert.ToInt32(dr["ConsecutiveFailures"]);
                    userLoginHistory.TotalFailures15Min = Convert.ToInt32(dr["TotalFailures15Min"]);
                    userLoginHistory.WasBlocked = Convert.ToBoolean(dr["WasBlocked"]);
                    userLoginHistory.CreatedOn = (DateTimeOffset)dr["CreatedOn"];
                    userLoginHistories.Add(userLoginHistory);
                }

            }
            return userLoginHistories;

        }

        public async Task<UserLoginHistory> GetUserLoginActivityByUserIdAsync(long userId)
        {
            UserLoginHistory userLoginHistory = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();


            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserLoginActivityByUserId]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();

            da.Fill(dt);

            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    userLoginHistory = new UserLoginHistory();
                    userLoginHistory.Id = Convert.ToInt64(item["Id"]);
                    userLoginHistory.UserId = item["UserId"] != DBNull.Value ? (long?)Convert.ToInt64(item["UserId"]) : null;
                    userLoginHistory.EmailAttempted = item["EmailAttempted"] != DBNull.Value ? item["EmailAttempted"].ToString() : null;
                    userLoginHistory.LoginAttemptTime = (DateTimeOffset)item["LoginAttemptTime"];
                    userLoginHistory.IsSuccess = item["IsSuccess"] != DBNull.Value ? Convert.ToBoolean(item["IsSuccess"]) : false;
                    userLoginHistory.StatusCode = item["StatusCode"] != DBNull.Value ? Convert.ToBoolean(item["StatusCode"]) : false;
                    userLoginHistory.StatusMessage = item["StatusMessage"] != DBNull.Value ? item["StatusMessage"].ToString() : null;
                    userLoginHistory.Notes = item["Notes"] != DBNull.Value ? item["Notes"].ToString() : null;
                    userLoginHistory.Problem = item["Problem"] != DBNull.Value ? item["Problem"].ToString() : null;
                    userLoginHistory.LoggedInFrom = item["LoggedInFrom"] != DBNull.Value ? item["LoggedInFrom"].ToString() : null;
                    userLoginHistory.IpAddress = item["IpAddress"] != DBNull.Value ? item["IpAddress"].ToString() : null;
                    userLoginHistory.UserAgent = item["UserAgent"] != DBNull.Value ? item["UserAgent"].ToString() : null;
                    userLoginHistory.FailedReason = item["FailedReason"] != DBNull.Value ? item["FailedReason"].ToString() : null;
                    userLoginHistory.ConsecutiveFailures = Convert.ToInt32(item["ConsecutiveFailures"]);
                    userLoginHistory.TotalFailures15Min = Convert.ToInt32(item["TotalFailures15Min"]);
                    userLoginHistory.WasBlocked = Convert.ToBoolean(item["WasBlocked"]);
                    userLoginHistory.CreatedOn = (DateTimeOffset)item["CreatedOn"];
                }
                
            }
            return userLoginHistory;

        }
    }
}
