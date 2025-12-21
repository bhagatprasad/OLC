using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmailRuleTypeController : ControllerBase
    {
        private readonly IEmailRuleTypeManager _emailRuleTypeManager;
        public EmailRuleTypeController(IEmailRuleTypeManager emailRuleTypeManager)
        {
            _emailRuleTypeManager = emailRuleTypeManager;
        }
        [HttpGet]
        [Route("GetAllEmailRuleTypesAsync")]
        public async Task<IActionResult> GetAllEmailRuleTypesAsync()
        {
            try
            {
                var response = await _emailRuleTypeManager.GetAllEmailRuleTypesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetEmailRuleTypeByIdAsync/{id}")]
        public async Task<IActionResult> GetEmailRuleTypeByIdAsync(long id)
        {
            try
            {
                var response = await _emailRuleTypeManager.GetEmailRuleTypeByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("InsertEmailRuleTypeAsync")]
        public async Task<IActionResult> InsertEmailRuleTypeAsync(Models.EmailRuleType emailRuleType)
        {
            try
            {
                var response = await _emailRuleTypeManager.InsertEmailRuleTypeAsync(emailRuleType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateEmailRuleTypeAsync")]
        public async Task<IActionResult> UpdateEmailRuleTypeAsync(Models.EmailRuleType emailRuleType)
        {
            try
            {
                var response = await _emailRuleTypeManager.UpdateEmailRuleTypeAsync(emailRuleType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("DeleteEmailRuleTypeAsync/{id}")]
        public async Task<IActionResult> DeleteEmailRuleTypeAsync(long id)
        {
            try
            {
                var response = await _emailRuleTypeManager.DeleteEmailRuleTypeAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
