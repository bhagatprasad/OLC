using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ICardTypeService
    {
        Task<bool> DeleteCardTypeAsync(long cardTypeId);
        Task<List<CardType>> GetCardTypeAsync();
        Task<CardType> GetCardTypeByIdAsync(long cardTypeId);
        Task<bool> InsertCardTypeAsync(CardType cardType);
        Task<bool> UpdateCardTypeAsync(CardType cardType);
    }
}
