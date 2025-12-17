using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class UserKycManager : IUserKycManager
    {
        private readonly string connectionString;
        public UserKycManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<UserKyc>> GetAllUsersKycAsync()
        {
            List<UserKyc> userKycs = new List<UserKyc>();
            UserKyc getuserKyc = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllUserKyc]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    getuserKyc = new UserKyc();

                    getuserKyc.Id = Convert.ToInt64(item["Id"]);
                    getuserKyc.UserId = Convert.ToInt64(item["UserId"]);
                    getuserKyc.KycStatus = item["KycStatus"] != DBNull.Value ? item["KycStatus"].ToString() : null;
                    getuserKyc.KycLevel = item["KycLevel"] != DBNull.Value ? item["KycLevel"].ToString() : null;
                    getuserKyc.SubmittedOn = item["SubmittedOn"] != DBNull.Value ? (DateTimeOffset?)item["SubmittedOn"] : null;
                    getuserKyc.VerifiedOn = item["VerifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["VerifiedOn"] : null;
                    getuserKyc.VerifiedBy = item["VerifiedBy"] != DBNull.Value ? Convert.ToInt64(item["VerifiedBy"]) : (long?)null;
                    getuserKyc.RejectionReason = item["RejectionReason"] != DBNull.Value ? item["RejectionReason"].ToString() : null;
                    getuserKyc.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : (long?)null;
                    getuserKyc.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getuserKyc.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : (long?)null;
                    getuserKyc.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getuserKyc.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                    userKycs.Add(getuserKyc);
                }
            }
            return userKycs;
        }
        public async Task<UserKyc> GetUserKycByUserIdAsync(long userId)
        {
            UserKyc getuserKyc = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserKycByUserId]", connection);

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
                    getuserKyc = new UserKyc();

                    getuserKyc.Id = Convert.ToInt64(item["Id"]);
                    getuserKyc.UserId = Convert.ToInt64(item["UserId"]);
                    getuserKyc.KycStatus = item["KycStatus"] != DBNull.Value ? item["KycStatus"].ToString() : null;
                    getuserKyc.KycLevel = item["KycLevel"] != DBNull.Value ? item["KycLevel"].ToString() : null;
                    getuserKyc.SubmittedOn = item["SubmittedOn"] != DBNull.Value ? (DateTimeOffset?)item["SubmittedOn"] : null;
                    getuserKyc.VerifiedOn = item["VerifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["VerifiedOn"] : null;
                    getuserKyc.VerifiedBy = item["VerifiedBy"] != DBNull.Value ? Convert.ToInt64(item["VerifiedBy"]) : (long?)null;
                    getuserKyc.RejectionReason = item["RejectionReason"] != DBNull.Value ? item["RejectionReason"].ToString() : null;
                    getuserKyc.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : (long?)null;
                    getuserKyc.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getuserKyc.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : (long?)null;
                    getuserKyc.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getuserKyc.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                }
            }
            return getuserKyc;
        }


        public async Task<bool> InsertUserKycAsync(UserKyc userKyc)
        {
            if (userKyc != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertUserKyc]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@userId", userKyc.UserId);

                sqlCommand.Parameters.AddWithValue("@kycStatus", userKyc.KycStatus);

                sqlCommand.Parameters.AddWithValue("@kycLevel", userKyc.KycLevel);

                sqlCommand.Parameters.AddWithValue("@submittedOn", userKyc.SubmittedOn);

                sqlCommand.Parameters.AddWithValue("@verifiedOn", userKyc.VerifiedOn);

                sqlCommand.Parameters.AddWithValue("@verifiedBy", userKyc.VerifiedBy);

                sqlCommand.Parameters.AddWithValue("@rejectionReason", userKyc.RejectionReason);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> ProcessUserKycAsync(UserKyc userKyc)
        {
            if (userKyc != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspProcessUserKyc]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", userKyc.Id);

                sqlCommand.Parameters.AddWithValue("@kycStatus", userKyc.KycStatus);

                sqlCommand.Parameters.AddWithValue("@kycLevel", userKyc.KycLevel);

                sqlCommand.Parameters.AddWithValue("@verifiedBy", userKyc.VerifiedBy);

                sqlCommand.Parameters.AddWithValue("@rejectionReason", userKyc.RejectionReason);

                sqlCommand.Parameters.AddWithValue("@modifiedBy", userKyc.ModifiedBy);

                sqlCommand.Parameters.AddWithValue("@isActive", userKyc.IsActive);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> VerifyUserKycAsync(VerifyUserKyc verifyUserKyc)
        {
            if (verifyUserKyc != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspVerifyUserKyc]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@userKycId", verifyUserKyc.UserKycId);

                sqlCommand.Parameters.AddWithValue("@userKycDocumentId", verifyUserKyc.UserKycDocumentId);

                sqlCommand.Parameters.AddWithValue("@status", verifyUserKyc.Status);

                sqlCommand.Parameters.AddWithValue("@modifiedBy", verifyUserKyc.ModifiedBy);

                sqlCommand.Parameters.AddWithValue("@rejectedReason", verifyUserKyc.RejectedReason);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }

}
