using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        [Route("GetUserAccountsAsync")]
        public async Task<IActionResult> GetUserAccountsAsync()
        {
            try
            {
                var response = await _userManager.GetUserAccountsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
