using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ICreditCardService
    {
        Task<List<UserCreditCard>> GetUserCreditCardsAsync(long userId);
        Task<bool> InsertUserCreditCardAsync(UserCreditCard userCreditCard);
        Task<UserCreditCard> GetUserCreditCardByCardIdAsync(long creditCardId);
        Task<bool> UpdateUserCreditCardAsync(UserCreditCard userCreditCard);
        Task<bool> DeleteUserCreditAsync(long creditcardId);
        Task<bool> ActivateUserCreditcard(UserCreditCard userCreditCard);
    }
}
