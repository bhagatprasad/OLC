using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public interface ICampaignTypeServices
    {
        Task<bool> InsertCampaignTypeAsync(CampaignType campaignType);
        Task<bool> UpdateCampaignTypeAsync(CampaignType campaignType);
        Task<List<CampaignType>> GetAllCampaignTypesAsync();
        Task<CampaignType> GetCampaignTypeByIdAsync(long id);
    }
}
