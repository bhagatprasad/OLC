using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace OLC.Web.API.Manager
{
    public class CardTypeManager : ICardTypeManager
    {
        private readonly string connectionString;
        public CardTypeManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<CardType> GetCardTypeByIdAsync(long Id)
        {
            CardType getCardTypeById = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCardTypeById]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", Id);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getCardTypeById = new CardType();

                    getCardTypeById.Id = Convert.ToInt64(item["Id"]);

                    getCardTypeById.Name = (item["Name"].ToString());

                    getCardTypeById.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getCardTypeById.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getCardTypeById.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getCardTypeById.CreatedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    getCardTypeById.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getCardTypeById.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }

            return getCardTypeById;
        }
        public async Task<List<CardType>> GetCardTypeAsync()
        {
            List<CardType> getCardTypes = new List<CardType>();

            CardType getCardType = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetCardTypes]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    getCardType = new CardType();

                    getCardType.Id = Convert.ToInt64(item["Id"]);

                    getCardType.Name = item["Name"].ToString();

                    getCardType.Code = item["Code"] != DBNull.Value ? item["Code"].ToString() : null;

                    getCardType.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    getCardType.CreatedOn = item["createdOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    getCardType.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    getCardType.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    getCardTypes.Add(getCardType);
                }
            }
            return getCardTypes;
        }
        public async Task<bool> InsertCardTypeAsync(CardType cardType)
        {
            if (cardType != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertCardType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@name", cardType.Name);

                sqlCommand.Parameters.AddWithValue("@code", cardType.Code);

                sqlCommand.Parameters.AddWithValue("@createdBy", cardType.CreatedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCardTypeAsync(CardType cardType)
        {
            if (cardType != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateCardType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", cardType.Id);

                sqlCommand.Parameters.AddWithValue("@name", cardType.Name);

                sqlCommand.Parameters.AddWithValue("@code", cardType.Code);

                sqlCommand.Parameters.AddWithValue("@modifiedBy", cardType.ModifiedBy);

                sqlCommand.Parameters.AddWithValue("@isActive", cardType.IsActive);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCardTypeAsync(long Id)
        {
            if (Id != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteCardType]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", Id);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }
}
