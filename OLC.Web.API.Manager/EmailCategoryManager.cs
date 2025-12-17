
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class EmailCategoryManager : IEmailCategoryManager
    {
        private readonly string connectionString;
        public EmailCategoryManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> DeleteEmailCategoryAsync(long id)
        {
            if (id != 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteEmailCategory]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id",id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<List<EmailCategory>> GetEmailCategoriesAsync()
        {
            List<EmailCategory> getEmaiCategories = new List<EmailCategory>();

            EmailCategory getEmailCategory = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllEmailCategories]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getEmailCategory = new EmailCategory();

                    getEmailCategory.Id = Convert.ToInt64(item["Id"]);

                    getEmailCategory.Name = item["Name"].ToString();

                    getEmailCategory.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getEmailCategory.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getEmailCategory.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getEmailCategory.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    getEmailCategory.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getEmailCategory.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getEmaiCategories.Add(getEmailCategory);
                }
            }
            return getEmaiCategories;
        }

        public async Task<EmailCategory> GetEmailCategoryAsync(long id)
        {

            EmailCategory getEmailCategory = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetEmailcategotyById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getEmailCategory = new EmailCategory();

                    getEmailCategory.Id = Convert.ToInt64(item["Id"]);

                    getEmailCategory.Name = item["Name"].ToString();

                    getEmailCategory.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getEmailCategory.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getEmailCategory.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getEmailCategory.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    getEmailCategory.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getEmailCategory.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                }      
            }
            return getEmailCategory;
        }

        public async Task<bool> InsertEmailCategoryAsync(EmailCategory emailCategory)
        {
            if (emailCategory != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertEmaiCategory]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@name", emailCategory.Name);
                sqlCommand.Parameters.AddWithValue("@code", emailCategory.Code);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async  Task<bool> UpdateEmailCategoryAsync(EmailCategory emailCategory)
        {
            if (emailCategory != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateEmaiCategory]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", emailCategory.Id);
                sqlCommand.Parameters.AddWithValue("@name", emailCategory.Name);
                sqlCommand.Parameters.AddWithValue("@code", emailCategory.Code);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}
