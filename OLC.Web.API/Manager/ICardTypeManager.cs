using OLC.Web.API.Models;

namespace OLC.Web.API.Manager
{
    public interface ICardTypeManager
    {
        Task<List<CardType>> GetCardTypeAsync();
        Task<CardType> GetCardTypeByIdAsync(long Id);
        Task <bool> InsertCardTypeAsync(CardType cardType);
        Task<bool> UpdateCardTypeAsync(CardType cardType);
        Task<bool> DeleteCardTypeAsync(long Id);
    }
}
