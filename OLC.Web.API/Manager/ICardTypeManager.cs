using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ICardTypeManager
    {
        Task<List<GetCardType>> GetUserCardTypeAsync(long createdBy);
        Task<GetCardType> GetUserCardTypeByIdAsync(long Id);
        Task <bool> InsertUserCardTypeAsync(CardType cardType);
        Task<bool> UpdateUserCardTypeAsync(UpdateCardType updateCardType);
        Task<bool> DeleteUserCardTypeAsync(long Id);
    }
}
