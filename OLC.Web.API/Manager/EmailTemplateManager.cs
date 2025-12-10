using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class EmailTemplateManager : IEmailTemplateManager
    {
        private readonly string connectionString;

        public EmailTemplateManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> DeleteEmailTemplateAsync(long id)
        {
            if (id != 0)
            {
                SqlConnection sqlConnection=new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand=new SqlCommand("[dbo].[uspDeleteTemplate]", sqlConnection);
                sqlCommand.CommandType=CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id",id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;    
            }
            return false;
        }

        public async Task<List<EmailTemplate>> GetAllTemplatesAsync()
        {
           List<EmailTemplate> emailtemplates = new List<EmailTemplate>();
           EmailTemplate emailTemplate = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllTemplate]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    emailTemplate = new EmailTemplate();

                    emailTemplate.Id = Convert.ToInt64(row["Id"]);

                    emailTemplate.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : null;

                    emailTemplate.Code = row["Code"] != DBNull.Value ? row["Code"].ToString() : null;

                    emailTemplate.Template = row["Template"] != DBNull.Value ? row["Template"].ToString() : null;

                    emailTemplate.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;

                    emailTemplate.CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null;

                    emailTemplate.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;

                    emailTemplate.ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null;

                    emailTemplate.IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null;

                    emailtemplates.Add(emailTemplate);
                }
            }
            return emailtemplates;
        }

        public async Task<EmailTemplate> GetEmailTemplateByIdAsync(long id)
        {
            EmailTemplate emailTemplate = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetTemplateById]", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@templateId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    emailTemplate = new EmailTemplate();

                    emailTemplate.Id = Convert.ToInt64(row["Id"]);

                    emailTemplate.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : null;

                    emailTemplate.Code = row["Code"] != DBNull.Value ? row["Code"].ToString() : null;

                    emailTemplate.Template = row["Template"] != DBNull.Value ? row["Template"].ToString() : null;

                    emailTemplate.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt64(row["CreatedBy"]) : null;

                    emailTemplate.CreatedOn = row["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)row["CreatedOn"] : null;

                    emailTemplate.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(row["ModifiedBy"]) : null;

                    emailTemplate.ModifiedOn = row["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)row["ModifiedOn"] : null;

                    emailTemplate.IsActive = row["IsActive"] != DBNull.Value ? (bool?)row["IsActive"] : null;
                }
            }
            return emailTemplate;
        }

        public async Task<bool> InsertEmailTemplateAsync(EmailTemplate emailtemplate)
        {
            if(emailtemplate!=null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertTemplate]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@name", emailtemplate.Name);
                sqlCommand.Parameters.AddWithValue("@code",emailtemplate.Code);
                sqlCommand.Parameters.AddWithValue("@template", emailtemplate.Template);
                sqlCommand.Parameters.AddWithValue("@createdBy",emailtemplate.CreatedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateEmailTemplateAsync(EmailTemplate emailTemplate)
        {
            if (emailTemplate != null) 
            {
               SqlConnection sqlConnection=new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateTemplate]", sqlConnection);
                sqlCommand.CommandType=CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id",emailTemplate.Id);
                sqlCommand.Parameters.AddWithValue("@name", emailTemplate.Name);
                sqlCommand.Parameters.AddWithValue("@code", emailTemplate.Code);
                sqlCommand.Parameters.AddWithValue("@template", emailTemplate.Template);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", emailTemplate.ModifiedBy);
                sqlCommand.Parameters.AddWithValue("@IsActive", emailTemplate.IsActive);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}
