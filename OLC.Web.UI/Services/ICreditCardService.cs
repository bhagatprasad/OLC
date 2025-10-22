using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ICreditCardService
    {
        Task<List<UserCreditCard>> GetUserCreditCardsAsync(long userId);
    }
}
