using OLC.Web.UI.Models;

namespace OLC.Web.UI.Services
{
    public class CampaignTypeServices : ICampaignTypeServices
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public CampaignTypeServices(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<List<CampaignType>> GetAllCampaignTypesAsync()
        {
            return await _repositoryFactory.SendAsync<List<CampaignType>>(HttpMethod.Get, "CampaignType/GetAllCampaignTypesAsync");
        }

        public async Task<CampaignType> GetCampaignTypeByIdAsync(long id)
        {
            var url = Path.Combine("CampaignType/GetCampaignTypeByIdAsync", id.ToString());
            return await _repositoryFactory.SendAsync<CampaignType>(HttpMethod.Get, url);
        }

        public async Task<bool> InsertCampaignTypeAsync(CampaignType campaignType)
        {
            return await _repositoryFactory.SendAsync<CampaignType, bool>(HttpMethod.Post, "CampaignType/InsertCampaignTypeAsync", campaignType);
        }       
        public async Task<bool> UpdateCampaignTypeAsync(CampaignType campaignType)
        {
            return await _repositoryFactory.SendAsync<CampaignType, bool>(HttpMethod.Post, "CampaignType/UpdateCampaignTypeAsync", campaignType);
        }
    }    
}
