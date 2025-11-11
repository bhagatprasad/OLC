using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace OLC.Web.API.Manager
{
    public class CreditCardManager : ICreditCardManager
    {
        private readonly string connectionString;
        public CreditCardManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<UserCreditCard> GetUserCreditCardByCardIdAsync(long creditCardId)
        {
            UserCreditCard userCreditCard = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserCreditCardByCardId]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@createdCardId", creditCardId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    userCreditCard = new UserCreditCard();

                        userCreditCard.Id = Convert.ToInt64(item["Id"]);

                    userCreditCard.UserId = Convert.ToInt64(item["UserId"]);

                    userCreditCard.CardHolderName = item["CardHolderName"] != DBNull.Value ? item["CardHolderName"].ToString() : null;

                    userCreditCard.EncryptedCardNumber = item["EncryptedCardNumber"] != DBNull.Value ? item["EncryptedCardNumber"].ToString() : null;

                    userCreditCard.MaskedCardNumber = item["MaskedCardNumber"] != DBNull.Value ? item["MaskedCardNumber"].ToString() : null;

                    userCreditCard.LastFourDigits = item["LastFourDigits"] != DBNull.Value ? item["LastFourDigits"].ToString() : null;

                    userCreditCard.ExpiryMonth = item["ExpiryMonth"] != DBNull.Value ? item["ExpiryMonth"].ToString() : null;

                    userCreditCard.ExpiryYear = item["ExpiryYear"] != DBNull.Value ? item["ExpiryYear"].ToString() : null;

                    userCreditCard.EncryptedCVV = item["EncryptedCVV"] != DBNull.Value ? item["EncryptedCVV"].ToString() : null;

                    userCreditCard.CardType = item["CardType"] != DBNull.Value ? item["CardType"].ToString() : null;

                    userCreditCard.IssuingBank = item["IssuingBank"] != DBNull.Value ? item["IssuingBank"].ToString() : null;

                    userCreditCard.IsDefault = item["IsDefault"] != DBNull.Value ? (bool?)item["IsDefault"] : null;

                    userCreditCard.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    userCreditCard.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    userCreditCard.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    userCreditCard.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    userCreditCard.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }

            }

            return userCreditCard;
        }


        public async Task<List<UserCreditCard>> GetUserCreditCardsAsync(long userId)
        {
            List<UserCreditCard> userCreditCards = new List<UserCreditCard>();

            UserCreditCard userCreditCard = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserCreditCards]", connection);

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

                    userCreditCard = new UserCreditCard();

                    userCreditCard.Id = Convert.ToInt64(item["Id"]);

                    userCreditCard.UserId = Convert.ToInt64(item["UserId"]);

                    userCreditCard.CardHolderName = item["CardHolderName"] != DBNull.Value ? item["CardHolderName"].ToString() : null;

                    userCreditCard.EncryptedCardNumber = item["EncryptedCardNumber"] != DBNull.Value ? item["EncryptedCardNumber"].ToString() : null;

                    userCreditCard.MaskedCardNumber = item["MaskedCardNumber"] != DBNull.Value ? item["MaskedCardNumber"].ToString() : null;

                    userCreditCard.LastFourDigits = item["LastFourDigits"] != DBNull.Value ? item["LastFourDigits"].ToString() : null;

                    userCreditCard.ExpiryMonth = item["ExpiryMonth"] != DBNull.Value ? item["ExpiryMonth"].ToString() : null;

                    userCreditCard.ExpiryYear = item["ExpiryYear"] != DBNull.Value ? item["ExpiryYear"].ToString() : null;

                    userCreditCard.EncryptedCVV = item["EncryptedCVV"] != DBNull.Value ? item["EncryptedCVV"].ToString() : null;

                    userCreditCard.CardType = item["CardType"] != DBNull.Value ? item["CardType"].ToString() : null;

                    userCreditCard.IssuingBank = item["IssuingBank"] != DBNull.Value ? item["IssuingBank"].ToString() : null;

                    userCreditCard.IsDefault = item["IsDefault"] != DBNull.Value ? (bool?)item["IsDefault"] : null;

                    userCreditCard.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    userCreditCard.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    userCreditCard.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    userCreditCard.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    userCreditCard.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;

                    userCreditCards.Add(userCreditCard);

                }

            }

            return userCreditCards;
        }

        public async Task<bool> InsertUserCreditCardAsync(UserCreditCard userCreditCard)
        {
            if (userCreditCard != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspInsertUserCreditCards]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@userId", userCreditCard.UserId);

                sqlCommand.Parameters.AddWithValue("@cardholderName", userCreditCard.CardHolderName);

                sqlCommand.Parameters.AddWithValue("@encryptedCardNumber", userCreditCard.EncryptedCardNumber);

                sqlCommand.Parameters.AddWithValue("@maskedCardNumber", userCreditCard.MaskedCardNumber);

                sqlCommand.Parameters.AddWithValue("@lastFourDigits", userCreditCard.LastFourDigits);

                sqlCommand.Parameters.AddWithValue("@expiryMonth", userCreditCard.ExpiryMonth);

                sqlCommand.Parameters.AddWithValue("@expiryYear", userCreditCard.ExpiryYear);

                sqlCommand.Parameters.AddWithValue("@encryptedCVV", userCreditCard.EncryptedCVV);

                sqlCommand.Parameters.AddWithValue("@cardType", userCreditCard.CardType);

                sqlCommand.Parameters.AddWithValue("@issuingBank", userCreditCard.IssuingBank);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserCreditCardAsync(UserCreditCard userCreditCard)
        {

            if (userCreditCard != null) ;
            {
                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspUpdateUserCreditCard]", connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@id", userCreditCard.Id);

                sqlCommand.Parameters.AddWithValue("@userId", userCreditCard.UserId);

                sqlCommand.Parameters.AddWithValue("@cardHolderName", userCreditCard.CardHolderName);

                sqlCommand.Parameters.AddWithValue("@encryptedCardNumber", userCreditCard.EncryptedCardNumber);

                sqlCommand.Parameters.AddWithValue("@maskedCardNumber", userCreditCard.MaskedCardNumber);

                sqlCommand.Parameters.AddWithValue("@lastFourDigits", userCreditCard.LastFourDigits);

                sqlCommand.Parameters.AddWithValue("@expiryMonth", userCreditCard.ExpiryMonth);

                sqlCommand.Parameters.AddWithValue("@expiryYear", userCreditCard.ExpiryYear);

                sqlCommand.Parameters.AddWithValue("@encryptedCVV", userCreditCard.EncryptedCVV);

                sqlCommand.Parameters.AddWithValue("@cardType", userCreditCard.CardType);

                sqlCommand.Parameters.AddWithValue("@issuingBank", userCreditCard.IssuingBank);

                sqlCommand.ExecuteNonQuery();

                connection.Close();

                return true;
            }

        }


        public async Task<bool> DeleteUserCreditAsync(long creditCardId)
        {
            if (creditCardId != 0) ;
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspDeleteUserCreditCard]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@creditCardId", creditCardId);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;

            }
        }

        public async Task<bool> ActivateUserCreditCardAsync(UserCreditCard userCreditCard)
        {
            if (userCreditCard != null)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("[dbo].[uspActivateUserCreditCard]", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@creditCardId", userCreditCard.Id);
                sqlCommand.Parameters.AddWithValue("@modifiedBy", userCreditCard.ModifiedBy);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                return true;
            }
            return false;
        }
    }
}