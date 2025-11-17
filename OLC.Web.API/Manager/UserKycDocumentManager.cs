using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class UserKycDocumentManager : IUserKycDocumentManager
    {
        private readonly string connectionString;
        public UserKycDocumentManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> UploadeUserKycDocumentAsync(UserKycDocument userKycDocument)
        {
            if(userKycDocument != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUploadeUserKycDocument]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@userId",userKycDocument.UserId);
                sqlCommand.Parameters.AddWithValue("@documentType", userKycDocument.DocumentType);
                sqlCommand.Parameters.AddWithValue("@documentNumber", userKycDocument.DocumentNumber);
                sqlCommand.Parameters.AddWithValue("@documentFilePath", userKycDocument.DocumentFilePath);
                sqlCommand.Parameters.Add("@documentFileData", SqlDbType.VarBinary).Value = userKycDocument.DocumentFileData == null ? DBNull.Value : userKycDocument.DocumentFileData;
                sqlCommand.Parameters.AddWithValue("@verifiedOn", userKycDocument.VerifiedOn);
                sqlCommand.Parameters.AddWithValue("@verifiedBy", userKycDocument.VerifiedBy);
                sqlCommand.Parameters.AddWithValue("@rejectionReason", userKycDocument.RejectionReason);
                sqlCommand.Parameters.AddWithValue("@expiryDate", userKycDocument.ExpiryDate);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserKycDocumentAsync(UserKycDocument userKycDocument)
        {
            if (userKycDocument != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateUserKycDocument]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@userId", userKycDocument.UserId);
                sqlCommand.Parameters.AddWithValue("@documentType", userKycDocument.DocumentType);
                sqlCommand.Parameters.AddWithValue("@documentNumber", userKycDocument.DocumentNumber);
                sqlCommand.Parameters.AddWithValue("@documentFilePath", userKycDocument.DocumentFilePath);
                sqlCommand.Parameters.Add("@documentFileData", SqlDbType.VarBinary).Value = userKycDocument.DocumentFileData == null ? DBNull.Value : userKycDocument.DocumentFileData;
                sqlCommand.Parameters.AddWithValue("@verificationStatus", userKycDocument.VerificationStatus);
                sqlCommand.Parameters.AddWithValue("@verifiedOn", userKycDocument.VerifiedOn);
                sqlCommand.Parameters.AddWithValue("@verifiedBy", userKycDocument.VerifiedBy);
                sqlCommand.Parameters.AddWithValue("@rejectionReason", userKycDocument.RejectionReason);
                sqlCommand.Parameters.AddWithValue("@expiryDate", userKycDocument.ExpiryDate);
                sqlCommand.Parameters.AddWithValue("@isActive", userKycDocument.IsActive);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<List<UserKycDocument>> GetAllUsersKycDocumentsAsync()
        {
            List<UserKycDocument> getAllUsersDocuments = new List<UserKycDocument>();

            UserKycDocument getAllUsesDocument = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllUsersKycDocuments]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow reader in dt.Rows)
                {
                    getAllUsesDocument = new UserKycDocument();

                    getAllUsesDocument.Id = Convert.ToInt64(reader["Id"]) ;

                    getAllUsesDocument.UserId = Convert.ToInt64(reader["UserId"]);

                    getAllUsesDocument.DocumentType = reader["DocumentType"] != DBNull.Value ? reader["DocumentType"].ToString() : null;

                    getAllUsesDocument.DocumentNumber = reader["DocumentNumber"] != DBNull.Value ? reader["DocumentNumber"].ToString() : null;

                    getAllUsesDocument.DocumentFilePath = reader["DocumentFilePath"] != DBNull.Value ? reader["DocumentFilePath"].ToString() : null;

                    getAllUsesDocument.DocumentFileData = reader["DocumentFileData"] != DBNull.Value ? (byte[])reader["DocumentFileData"] : null;

                    getAllUsesDocument.VerificationStatus = reader["VerificationStatus"] != DBNull.Value ? reader["VerificationStatus"].ToString() : null;

                    getAllUsesDocument.VerifiedOn = reader["VerifiedOn"] != DBNull.Value ? (DateTimeOffset?)reader["VerifiedOn"] : null;

                    getAllUsesDocument.VerifiedBy = reader["VerifiedBy"] != DBNull.Value ? Convert.ToInt64(reader["VerifiedBy"]) : (long?)null;

                    getAllUsesDocument.RejectionReason = reader["RejectionReason"] != DBNull.Value ? reader["RejectionReason"].ToString() : null;

                    getAllUsesDocument.ExpiryDate = reader["ExpiryDate"] != DBNull.Value ? (DateTime?)reader["ExpiryDate"] : null;

                    getAllUsesDocument.CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt64(reader["CreatedBy"]) : (long?)null;

                    getAllUsesDocument.CreatedOn = reader["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)reader["CreatedOn"] : null;

                    getAllUsesDocument.ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(reader["ModifiedBy"]) : (long?)null;

                    getAllUsesDocument.ModifiedOn = reader["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)reader["ModifiedOn"] : null;

                    getAllUsesDocument.IsActive = reader["IsActive"] != DBNull.Value ? (bool?)reader["IsActive"] : null;

                    getAllUsersDocuments.Add(getAllUsesDocument);

                }
            }
            return getAllUsersDocuments;
        }
    } 
}
