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
    public class CampaignTypeManager : ICampaignTypeManager
    {
        private readonly string connectionString;
        public CampaignTypeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<CampaignType>> GetAllCampaignTypesAsync()
        {
            List<CampaignType> getCampaignTypes = new List<CampaignType>();

            CampaignType getCampaignType = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllCampaignTypes]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getCampaignType = new CampaignType();

                    getCampaignType.Id = Convert.ToInt64(item["Id"]);
                    getCampaignType.Name = item["Name"] != DBNull.Value ? item["Name"].ToString() : null;
                    getCampaignType.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;
                    getCampaignType.Description = item["Description"] != DBNull.Value ? item["Description"].ToString() : null;
                    getCampaignType.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    getCampaignType.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getCampaignType.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getCampaignType.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getCampaignType.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getCampaignTypes.Add(getCampaignType);
                }
            }

            return getCampaignTypes;
        }

        public async Task<CampaignType> GetCampaignTypeByIdAsync(long id)
        {
            CampaignType getCampaignType = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCampaignTypeById]", connection);

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

                    getCampaignType = new CampaignType();

                    getCampaignType.Id = Convert.ToInt64(item["Id"]);
                    getCampaignType.Name = item["Name"] != DBNull.Value ? item["Name"].ToString() : null;
                    getCampaignType.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;
                    getCampaignType.Description = item["Description"] != DBNull.Value ? item["Description"].ToString() : null;
                    getCampaignType.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;
                    getCampaignType.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;
                    getCampaignType.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;
                    getCampaignType.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;
                    getCampaignType.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return getCampaignType;
        }

        public async Task<bool> InsertCampaignTypeAsync(CampaignType campaignType)
        {
            if (campaignType != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspInsertCampaignType]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", campaignType.Name);
                cmd.Parameters.AddWithValue("@Code", campaignType.Code);
                cmd.Parameters.AddWithValue("@Description", campaignType.Description);
                cmd.Parameters.AddWithValue("@CreatedBy", campaignType.CreatedBy);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCampaignTypeAsync(CampaignType campaignType)
        {
            if (campaignType != null)
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("[dbo].[uspUpdateCampaignType]", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", campaignType.Id);
                cmd.Parameters.AddWithValue("@Name", campaignType.Name);
                cmd.Parameters.AddWithValue("@Code", campaignType.Code);
                cmd.Parameters.AddWithValue("@Description", campaignType.Description);
                cmd.Parameters.AddWithValue("@ModifiedBy", campaignType.ModifiedBy);

                cmd.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }

            return false;
        }
    }
}
