using OLC.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC.Web.API.Manager
{
    public interface IEmailCampaignManager
    {
        Task<bool> InsertEmailCampaignAsync(EmailCampaign emailCampaign);
        Task<bool> UpdateEmailCampaignAsync(EmailCampaign emailCampaign);
        Task<List<EmailCampaign>> GetAllEmailCampaignsAsync();
        Task<EmailCampaign> GetEmailCampaignByIdAsync(long id);

    }
}
