using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ICreditCardManager
    {
        Task<List<UserCreditCard>> GetUserCreditCardsAsync(long userId);
        Task<bool> InsertUserCreditCardAsync(UserCreditCard userCreditCard);
        Task<UserCreditCard> GetUserCreditCardByCardIdAsync(long creditCardId);
        Task<bool> UpdateUserCreditCardAsync(UserCreditCard userCreditCard);
        Task<bool> DeleteUserCreditAsync(long creditcardId);
        Task<bool> ActivateUserCreditCardAsync(UserCreditCard userCreditCard);
    }
}
