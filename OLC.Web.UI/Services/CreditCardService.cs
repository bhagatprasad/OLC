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
        public async Task<List<UserCreditCard>> GetUserCreditCardsAsync(long userId)
        {
            var url = Path.Combine("CreditCard/GetUserCreditCardsAsync", userId.ToString());

            return await _repositoryFactory.SendAsync<List<UserCreditCard>>(HttpMethod.Get, url);
        }
    }
}
