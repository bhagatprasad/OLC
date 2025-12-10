using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsLetterController : ControllerBase
    {
        private readonly INewsLetterManager _newsLetterManager;

        public NewsLetterController(INewsLetterManager newsLetterManager)
        {
            _newsLetterManager = newsLetterManager;
        }

        [HttpGet]
        [Route("GetNewsLettersAsync")]
        public async Task<IActionResult> GetNewsLettersAsync()
        {
            try
            {
                var response = await _newsLetterManager.GetNewsLettersAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertNewsLetterAsync")]
        public async Task <IActionResult>InsertNewsLetterAsync(NewsLetter newsLetter)
        {
            try
            {
                var response = await _newsLetterManager.InsertNewsLetterAsync(newsLetter);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateNewsLetterAsync")]
        public async Task <IActionResult> UpdateNewsLetterAsync(NewsLetter newsLetter)
        {
            try
            {
                var response = await _newsLetterManager.UpdateNewsLetterAsync(newsLetter);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
