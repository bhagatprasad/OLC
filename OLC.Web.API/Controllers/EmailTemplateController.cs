using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateManager _emailTemplateManager;

        public EmailTemplateController(IEmailTemplateManager emailTemplateManager)
        {
            _emailTemplateManager = emailTemplateManager;
        }
        [HttpGet]
        [Route("GetAllTemplatesAsync")]
        public async Task<IActionResult> GetAllTemplatesAsync()
        {
            try
            {
                var templateResponse = await _emailTemplateManager.GetAllTemplatesAsync();
                return Ok(templateResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetEmailTemplateByIdAsync/{id}")]
        public async Task<IActionResult> GetEmailTemplateByIdAsync(long id)
        {
            try
            {
                var templateResponse = await _emailTemplateManager.GetEmailTemplateByIdAsync(id);
                return Ok(templateResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteEmailTemplateAsync/{id}")]
        public async Task<IActionResult> DeleteEmailTemplateAsync(long id)
        {
            try
            {
                var templateResponse = await _emailTemplateManager.DeleteEmailTemplateAsync(id);
                return Ok(templateResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("InsertEmailTemplateAsync")]
        public async Task<IActionResult> InsertEmailTemplateAsync(EmailTemplate emailTemplate)
        {
            try
            {
                var templateResponse = await _emailTemplateManager.InsertEmailTemplateAsync(emailTemplate);
                return Ok(templateResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateEmailTemplateAsync")]
        public async Task<IActionResult> UpdateEmailTemplateAsync(EmailTemplate emailTemplate)
        {
            try
            {
                var templateResponse = await _emailTemplateManager.UpdateEmailTemplateAsync(emailTemplate);
                return Ok(templateResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
