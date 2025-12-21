using OLC.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC.Web.API.Manager
{
    public interface ICampaignTypeManager
    {
        Task<bool> InsertCampaignTypeAsync(CampaignType campaignType);
        Task<bool> UpdateCampaignTypeAsync(CampaignType campaignType);
        Task<List<CampaignType>> GetAllCampaignTypesAsync();
        Task<CampaignType> GetCampaignTypeByIdAsync(long id);
    }
}
