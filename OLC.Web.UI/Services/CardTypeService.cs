using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class CardTypeService : ICardTypeService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public CardTypeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteCardTypeAsync(long cardTypeId)
        {
            var url = Path.Combine("CardType/DeleteCardTypeAsync", cardTypeId.ToString());
            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public async Task<List<CardType>> GetCardTypeAsync()
        {
            return await _repositoryFactory.SendAsync<List<CardType>>(HttpMethod.Get, "CardType/GetCardTypeAsync");
        }

        public async Task<CardType> GetCardTypeByIdAsync(long cardTypeId)
        {
            var url = Path.Combine("CardType/GetCardTypeByIdAsync", cardTypeId.ToString());
            return await _repositoryFactory.SendAsync<CardType>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertCardTypeAsync(CardType cardType)
        {
            return await _repositoryFactory.SendAsync<CardType, bool>(HttpMethod.Post, "CardType/InsertCardTypeAsync", cardType);
        }

        public async Task<bool> UpdateCardTypeAsync(CardType cardType)
        {
            return await _repositoryFactory.SendAsync<CardType, bool>(HttpMethod.Post, "CardType/UpdateCardTypeAsync", cardType);
        }
    }
}
