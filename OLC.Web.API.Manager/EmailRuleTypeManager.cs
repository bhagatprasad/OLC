using OLC.Web.API.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace OLC.Web.API.Manager
{
    public class EmailRuleTypeManager : IEmailRuleTypeManager
    {
        private readonly string connectionString;

        public EmailRuleTypeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async  Task<bool> DeleteEmailRuleTypeAsync(long id)
        {
            if (id != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteEmailRuleType]", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        
        public async Task<List<EmailRuleType>> GetAllEmailRuleTypesAsync()
        {
         List<EmailRuleType> emailRuleTypeList =new List<EmailRuleType>();

            EmailRuleType emailRuleType = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllEmailRuleTypes]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();

            if(dt.Rows.Count >0)
            {
                foreach(DataRow dr in dt.Rows)
                {

                    emailRuleType =new EmailRuleType();

                    emailRuleType.Id = Convert.ToInt32(dr["Id"]);
                    emailRuleType.RuleCode = dr["RuleCode"].ToString();
                    emailRuleType.RuleName = dr["RuleName"].ToString();
                    emailRuleType.Description = dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null;
                    emailRuleType.CreatedOn = (DateTimeOffset)dr["CreatedOn"];
                    emailRuleType.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt32(dr["CreatedBy"]) : null;
                    emailRuleType.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    emailRuleType.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(dr["ModifiedBy"]) : null;
                    emailRuleType.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    emailRuleTypeList.Add(emailRuleType);
                }
            }
            return emailRuleTypeList;

        }

        public async Task<EmailRuleType> GetEmailRuleTypeByIdAsync(long id)
        {
            EmailRuleType emailRuleType = null;

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetEmailRuleTypeById]", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    emailRuleType = new EmailRuleType();

                    emailRuleType.Id = Convert.ToInt32(dr["Id"]);
                    emailRuleType.RuleCode = dr["RuleCode"].ToString();
                    emailRuleType.RuleName = dr["RuleName"].ToString();
                    emailRuleType.Description = dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null;
                    emailRuleType.CreatedOn = (DateTimeOffset)dr["CreatedOn"];
                    emailRuleType.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt32(dr["CreatedBy"]) : null;
                    emailRuleType.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset)dr["ModifiedOn"] : null;
                    emailRuleType.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(dr["ModifiedBy"]) : null;
                    emailRuleType.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    return emailRuleType;
                }
            }
            return emailRuleType;
        }

        public async Task<bool> InsertEmailRuleTypeAsync(EmailRuleType emailRuleType)
        {

            if (emailRuleType != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertEmailRuleType]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
               
                sqlCommand.Parameters.AddWithValue("@ruleCode", emailRuleType.RuleCode);
                sqlCommand.Parameters.AddWithValue("@ruleName", emailRuleType.RuleName);
                sqlCommand.Parameters.AddWithValue("@description", emailRuleType.Description);
                sqlCommand.Parameters.AddWithValue("@createdBy", emailRuleType.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async  Task<bool> UpdateEmailRuleTypeAsync(EmailRuleType emailRuleType)
        {
            if (emailRuleType != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateEmailRuleType]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                
                sqlCommand.Parameters.AddWithValue("@id", emailRuleType.Id);
                sqlCommand.Parameters.AddWithValue("@ruleCode", emailRuleType.RuleCode);
                sqlCommand.Parameters.AddWithValue("@ruleName", emailRuleType.RuleName);
                sqlCommand.Parameters.AddWithValue("@description", emailRuleType.Description);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", emailRuleType.ModifiedBy);
                sqlCommand.Parameters.AddWithValue("@isActive", emailRuleType.IsActive);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
                return true;
            }
            return false;

        }

    }
}
