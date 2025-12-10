using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailCategoryController : ControllerBase
    {
        private readonly IEmailCategoryManager _emailCategoryManager;

        public EmailCategoryController(IEmailCategoryManager emailCategoryManager)
        {
            _emailCategoryManager = emailCategoryManager;
        }

        [HttpGet]
        [Route("GetEmailCategoriesAsync")]
        public async Task<IActionResult> GetEmailCategoriesAsync()
        {
            try
            {
                var response = await _emailCategoryManager.GetEmailCategoriesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetEmailCategoryById/{id}")]
        public async Task<IActionResult> GetEmailCategoriesByIdAsync(long id)
        {
            try
            {
                var response = await _emailCategoryManager.GetEmailCategoryAsync(id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("InsertEmailCategoryAsync")]
        public async Task<IActionResult> InsertEmailCategoryAsync(EmailCategory emailCategory)
        {
            try
            {
                var response = await _emailCategoryManager.InsertEmailCategoryAsync(emailCategory);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateEmailCategoryAsync")]
        public async Task<IActionResult> UpdateEmailCategoryAsync(EmailCategory emailCategory)
        {
            try
            {
                var response = await _emailCategoryManager.UpdateEmailCategoryAsync(emailCategory);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteEmailCategoryAsync/")]
        public async Task<IActionResult> DeleteEmailCategoryAsync(long id)
        {
            try
            {
                var response = await _emailCategoryManager.DeleteEmailCategoryAsync(id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
