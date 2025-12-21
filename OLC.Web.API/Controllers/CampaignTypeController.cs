using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignTypeController : ControllerBase
    {
        private readonly ICampaignTypeManager _campaignTypeManager;

        public CampaignTypeController(ICampaignTypeManager campaignTypeManager)
        {
            _campaignTypeManager = campaignTypeManager;
        }

        [HttpGet]
        [Route("GetAllCampaignTypesAsync")]
        public async Task<IActionResult> GetAllCampaignTypesAsync()
        {
            try
            {
                var response = await _campaignTypeManager.GetAllCampaignTypesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCampaignTypeByIdAsync/{id}")]
        public async Task<IActionResult> GetCampaignTypeByIdAsync(long id)
        {
            try
            {
                var response = await _campaignTypeManager.GetCampaignTypeByIdAsync(id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertCampaignTypeAsync")]
        public async Task<IActionResult> InsertCampaignTypeAsync(CampaignType campaignType)
        {
            try
            {
                var response = await _campaignTypeManager.InsertCampaignTypeAsync(campaignType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateCampaignTypeAsync")]
        public async Task<IActionResult> UpdateCampaignTypeAsync(CampaignType campaignType)
        {
            try
            {
                var response = await _campaignTypeManager.UpdateCampaignTypeAsync(campaignType);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
