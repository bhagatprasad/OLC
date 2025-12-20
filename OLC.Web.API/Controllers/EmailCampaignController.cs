using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailCampaignController : ControllerBase
    {
        private readonly IEmailCampaignManager _emailCampaignManager;

        public EmailCampaignController(IEmailCampaignManager emailCampaignManager)
        {
            _emailCampaignManager = emailCampaignManager;
        }

        [HttpGet]
        [Route("GetAllEmailCampaignsAsync")]
        public async Task<IActionResult> GetAllEmailCampaignsAsync()
        {
            try
            {
                var response = await _emailCampaignManager.GetAllEmailCampaignsAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetEmailCampaignByIdAsync/{id}")]
        public async Task<IActionResult> GetEmailCampaignByIdAsync(long id)
        {
            try
            {
                var response = await _emailCampaignManager.GetEmailCampaignByIdAsync(id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertEmailCampaignAsync")]
        public async Task<IActionResult> InsertEmailCampaignAsync(EmailCampaign emailCampaign )
        {
            try
            {
                var response = await _emailCampaignManager.InsertEmailCampaignAsync(emailCampaign);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateEmailCampaignAsync")]
        public async Task<IActionResult> UpdateEmailCampaignAsync(EmailCampaign emailCampaign)
        {
            try
            {
                var response = await _emailCampaignManager.UpdateEmailCampaignAsync(emailCampaign);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
