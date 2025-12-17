using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OLC.Web.API.Manager
{
    public class NewsLetterManager : INewsLetterManager
    {
        private readonly string connectionString;

        public NewsLetterManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<NewsLetter>> GetNewsLettersAsync()
        {
            List<NewsLetter> newsLetters = new List<NewsLetter>();
            NewsLetter newsLetter= null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetAllNewsLetter]",sqlConnection);
            sqlCommand.CommandType =CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter=new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    newsLetter = new NewsLetter();

                    newsLetter.Id = Convert.ToInt64(dr["Id"]);

                    newsLetter.Email= dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null;

                    newsLetter.SubscribedOn = dr["SubscribedOn"] != DBNull.Value ? (DateTime?)dr["SubscribedOn"] : null;

                    newsLetter.UnsubscribedOn = dr["UnsubscribedOn"] != DBNull.Value ? (DateTime?)dr["UnsubscribedOn"] : null;

                    newsLetter.CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToInt64(dr["CreatedBy"]) : null;

                    newsLetter.CreatedOn = dr["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)dr["CreatedOn"] : null;

                    newsLetter.ModifiedBy = dr["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(dr["ModifiedBy"]) : null;

                    newsLetter.ModifiedOn = dr["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)dr["ModifiedOn"] : null;

                    newsLetter.IsActive = dr["IsActive"] != DBNull.Value ? (bool?)dr["IsActive"] : null;

                    newsLetters.Add(newsLetter);
                }
            }
            return newsLetters;
        }

        public async Task<bool> InsertNewsLetterAsync(NewsLetter newsLetter)
        {
            if(newsLetter!=null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertNewsLetter]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Email", newsLetter.Email);
                sqlCommand.Parameters.AddWithValue("@SubscribedOn", newsLetter.SubscribedOn);
                sqlCommand.Parameters.AddWithValue("@UnSubscribedOn", newsLetter.UnsubscribedOn);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", newsLetter.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@IsActive", newsLetter.IsActive);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateNewsLetterAsync(NewsLetter newsLetter)
        {
            if (newsLetter != null)
            {
                SqlConnection sqlConnection=new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateNewsLetter]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;   
                sqlCommand.Parameters.AddWithValue("@Id",newsLetter.Id);
                sqlCommand.Parameters.AddWithValue("@Email", newsLetter.Email);
                sqlCommand.Parameters.AddWithValue("@SubscribedOn",newsLetter.SubscribedOn);
                sqlCommand.Parameters.AddWithValue("@UnsubscribedOn",newsLetter.UnsubscribedOn);
                sqlCommand.Parameters.AddWithValue("@IsActive", newsLetter.IsActive);
                sqlCommand.Parameters.AddWithValue("@ModifiedBy", newsLetter.ModifiedBy);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            return false;
        }
    }
}
