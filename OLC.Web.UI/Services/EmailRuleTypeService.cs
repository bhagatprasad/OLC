using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class EmailRuleTypeService : IEmailRuleTypeService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public EmailRuleTypeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public Task<bool> DeleteEmailRuleTypeAsync(long id)
        {
            return _repositoryFactory.SendAsync<long, bool>(HttpMethod.Post, "EmailRuleType/DeleteEmailRuleTypeAsync", id);
        }

        public async Task<List<EmailRuleType>> GetAllEmailRuleTypesAsync()
        {
            return await  _repositoryFactory.SendAsync<List<EmailRuleType>>(HttpMethod.Get, "EmailRuleType/GetAllEmailRuleTypesAsync");
        }

        public Task<EmailRuleType> GetEmailRuleTypeByIdAsync(long id)
        {
           var url = Path.Combine("EmailRuleType/GetEmailRuleTypeByIdAsync", id.ToString());
              return  _repositoryFactory.SendAsync<EmailRuleType>(HttpMethod.Get, url);
        }

        public Task<bool> InsertEmailRuleTypeAsync(EmailRuleType emailRuleType)
        {
           return _repositoryFactory.SendAsync<EmailRuleType, bool>(HttpMethod.Post, "EmailRuleType/InsertEmailRuleTypeAsync", emailRuleType);
        }

        public Task<bool> UpdateEmailRuleTypeAsync(EmailRuleType emailRuleType)
        {
            return _repositoryFactory.SendAsync<EmailRuleType, bool>(HttpMethod.Post, "EmailRuleType/UpdateEmailRuleTypeAsync", emailRuleType);
        }
    }
}
