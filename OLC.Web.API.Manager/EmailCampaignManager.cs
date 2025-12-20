using Microsoft.Extensions.Configuration;
using OLC.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC.Web.API.Manager
{
    public class EmailCampaignManager : IEmailCampaignManager
    {
        private readonly string connectionString;
        public EmailCampaignManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<EmailCampaign>> GetAllEmailCampaignsAsync()
        {
            List<EmailCampaign> getEmailCampaigns = new List<EmailCampaign>();

            EmailCampaign getEmailCampaign = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllEmailCampaigns]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getEmailCampaign = new EmailCampaign();

                    getEmailCampaign.Id = Convert.ToInt64(item["Id"]);
                    getEmailCampaign.CampaignName = item["CampaignName"] != DBNull.Value ? item["CampaignName"].ToString() : null;
                    getEmailCampaign.CampaignType = item["CampaignType"] != DBNull.Value ? item["CampaignType"].ToString() : null;
                    getEmailCampaign.Description = item["Description"] != DBNull.Value ? item["Description"].ToString() : null;
                    getEmailCampaign.StartDate = item["StartDate"] != DBNull.Value ? (DateTimeOffset?)item["StartDate"] : null;
                    getEmailCampaign.EndDate = item["EndDate"] != DBNull.Value ? (DateTimeOffset?)item["EndDate"] : null;
                    getEmailCampaign.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    getEmailCampaign.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getEmailCampaign.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getEmailCampaign.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getEmailCampaign.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getEmailCampaigns.Add(getEmailCampaign);
                }
            }

            return getEmailCampaigns;
        }

        public async Task<EmailCampaign> GetEmailCampaignByIdAsync(long id)
        {
            EmailCampaign getEmailCampaign = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetEmailCampaignById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getEmailCampaign = new EmailCampaign();

                    getEmailCampaign.Id = Convert.ToInt64(item["Id"]);
                    getEmailCampaign.CampaignName = item["CampaignName"] != DBNull.Value ? item["CampaignName"].ToString() : null;
                    getEmailCampaign.CampaignType = item["CampaignType"] != DBNull.Value ? item["CampaignType"].ToString() : null;
                    getEmailCampaign.Description = item["Description"] != DBNull.Value ? item["Description"].ToString() : null;
                    getEmailCampaign.StartDate = item["StartDate"] != DBNull.Value ? (DateTimeOffset?)item["StartDate"] : null;
                    getEmailCampaign.EndDate = item["EndDate"] != DBNull.Value ? (DateTimeOffset?)item["EndDate"] : null;
                    getEmailCampaign.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    getEmailCampaign.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getEmailCampaign.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getEmailCampaign.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getEmailCampaign.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return getEmailCampaign;
        }

        public async Task<bool> InsertEmailCampaignAsync(EmailCampaign emailCampaign)
        {
            if (emailCampaign != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspInsertEmailCampaign]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CampaignName", emailCampaign.CampaignName) ;
                cmd.Parameters.AddWithValue("@CampaignType", emailCampaign.CampaignType);
                cmd.Parameters.AddWithValue("@Description", emailCampaign.Description);
                cmd.Parameters.AddWithValue("@StartDate", emailCampaign.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", emailCampaign.EndDate   );
                cmd.Parameters.AddWithValue("@CreatedBy", emailCampaign.CreatedBy);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEmailCampaignAsync(EmailCampaign emailCampaign)
        {
            if (emailCampaign != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspUpdateEmailCampaign]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", emailCampaign.Id);
                cmd.Parameters.AddWithValue("@CampaignName", emailCampaign.CampaignName);
                cmd.Parameters.AddWithValue("@CampaignType", emailCampaign.CampaignType);
                cmd.Parameters.AddWithValue("@Description", emailCampaign.Description);
                cmd.Parameters.AddWithValue("@StartDate", emailCampaign.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", emailCampaign.EndDate);
                cmd.Parameters.AddWithValue("@ModifiedBy", emailCampaign.ModifiedBy);
                cmd.Parameters.AddWithValue("@IsActive", emailCampaign.IsActive);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }
    }
}
