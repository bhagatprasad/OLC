using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public CreditCardService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> DeleteUserCreditAsync(long creditcardId)
        {
            var url = Path.Combine("CreditCard/DeleteUserCreditAsync", creditcardId.ToString());

            return await _repositoryFactory.SendAsync<bool>(HttpMethod.Delete, url);
        }

        public Task<UserCreditCard> GetUserCreditCardByCardIdAsync(long creditCardId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserCreditCard>> GetUserCreditCardsAsync(long userId)
        {
            var url = Path.Combine("CreditCard/GetUserCreditCardsAsync", userId.ToString());

            return await _repositoryFactory.SendAsync<List<UserCreditCard>>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertUserCreditCardAsync(UserCreditCard userCreditCard)
        {
            return await _repositoryFactory.SendAsync<UserCreditCard,bool>(HttpMethod.Post, "CreditCard/SaveUserCreditCardAsync", userCreditCard);
        }

        public async Task<bool> UpdateUserCreditCardAsync(UserCreditCard userCreditCard)
        {
            return await _repositoryFactory.SendAsync<UserCreditCard, bool>(HttpMethod.Post, "CreditCard/UpdateUserCreditCardAsync", userCreditCard);
        }
    }
}
