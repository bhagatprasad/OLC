using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ICardTypeManager
    {
        Task<List<CardType>> GetUserCardTypeAsync();
        Task<CardType> GetUserCardTypeByIdAsync(long Id);
        Task <bool> InsertUserCardTypeAsync(CardType cardType);
        Task<bool> UpdateUserCardTypeAsync(CardType cardType);
        Task<bool> DeleteUserCardTypeAsync(long Id);
    }
}
