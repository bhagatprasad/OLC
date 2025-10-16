using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ICreditCardManager
    {
        Task<List<UserCreditCard>> GetUserCreditCardsAsync(long userId);
        Task<bool> InsertUserCreditCardAsync(UserCreditCard userCreditCard);
        Task<UserCreditCard> GetUserCreditCardByCardIdAsync(long creditCardId);
    }
}
