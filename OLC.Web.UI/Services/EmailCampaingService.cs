using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class EmailCampaingService : IEmailCampaingService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public EmailCampaingService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<EmailCampaign>> GetAllEmailCampaignsAsync()
        {
            return await _repositoryFactory.SendAsync<List<EmailCampaign>>(HttpMethod.Get, "EmailCampaign/GetAllEmailCampaignsAsync");
        }

        public async Task<EmailCampaign> GetEmailCampaignByIdAsync(long id)
        {
            var url = Path.Combine("EmailCampaign/GetEmailCampaignByIdAsync", id.ToString());
            return await _repositoryFactory.SendAsync<EmailCampaign>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertEmailCampaignAsync(EmailCampaign emailCampaign)
        {
            return await _repositoryFactory.SendAsync<EmailCampaign, bool>(HttpMethod.Post, "EmailCampaign/InsertEmailCampaignAsync", emailCampaign);
        }

        public async Task<bool> UpdateEmailCampaignAsync(EmailCampaign emailCampaign)
        {
            return await _repositoryFactory.SendAsync<EmailCampaign, bool>(HttpMethod.Post, "EmailCampaign/UpdateEmailCampaignAsync", emailCampaign);
        }
    }
}
