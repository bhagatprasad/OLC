using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface IEmailCampaingService
    {
        Task<bool> InsertEmailCampaignAsync(EmailCampaign emailCampaign);
        Task<bool> UpdateEmailCampaignAsync(EmailCampaign emailCampaign);
        Task<List<EmailCampaign>> GetAllEmailCampaignsAsync();
        Task<EmailCampaign> GetEmailCampaignByIdAsync(long id);
    }
}
