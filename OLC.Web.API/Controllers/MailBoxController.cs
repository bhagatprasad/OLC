using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Diagnostics.Eventing.Reader;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailBoxController : ControllerBase
    {
        private readonly IMailBoxManager _mailBoxManager;

        public MailBoxController(IMailBoxManager mailBoxManager)
        {
           _mailBoxManager = mailBoxManager;
        }

        [HttpGet]
        [Route("GetAllMailBoxesAsync")]
        public async Task<IActionResult> GetAllMailBoxesAsync()
        {
            try
            {
                var response = await _mailBoxManager.GetAllMailBoxesAsync();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllMailBoxesAsync/{id}")]
        public async Task<IActionResult> GetAllMailBoxesAsync(long id)
        {
            try
            {
                var response = await _mailBoxManager.GetMailBoxByIdAsync(id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertMailBoxAsync")]
        public async Task<IActionResult> InsertMailBoxAsync(MailBox mailbox)
        {
            try
            {
                var response = await _mailBoxManager.InsertMailBoxAsync(mailbox);
                return Ok(response);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
