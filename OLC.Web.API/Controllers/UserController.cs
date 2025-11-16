using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Runtime.InteropServices;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IUserKycManager _userKycManager;
        private readonly IUserKycDocumentManager _userKycDocumentManager;
        public UserController(IUserManager userManager,IUserKycManager userKycManager, IUserKycDocumentManager userKycDocumentManager)
        {
            _userManager = userManager;
            _userKycManager = userKycManager;
            _userKycDocumentManager = userKycDocumentManager;
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
        [HttpPost]
        [Route("InserUserKycAsync")]
        public async Task<IActionResult> InserUserKycAsync(UserKyc userKyc)
        {
            try
            {
                var response = await _userKycManager.InsertUserKycAsync(userKyc);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ProcessUserKyAsync")]
        public async Task<IActionResult> ProcessUserKyAsync(UserKyc userKyc)
        {
            try
            {
                var response = await _userKycManager.ProcessUserKycAsync(userKyc);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserKycByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetUserKycByUserIdAsync(long userId)
        {
            try
            {
                var response = await _userKycManager.GetUserKycByUserIdAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllUsersKycAsync")]
        public async Task<IActionResult> GetAllUsersKycAsync()
        {
            try
            {
                var response = await _userKycManager.GetAllUsersKycAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UploadeUserKycDocumenAsync")]
        public async Task<IActionResult> UploadeUserKycDocumenAsync(UserKycDocument userKycDocument)
        {
            try
            {
                var response = await _userKycDocumentManager.UploadeUserKycDocumentAsync(userKycDocument);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateUserKycDocumenAsync")]
        public async Task<IActionResult> UpdateUserKycDocumenAsync(UserKycDocument userKycDocument)
        {
            try
            {
                var response = await _userKycDocumentManager.UpdateUserKycDocumentAsync(userKycDocument);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAllUsersKycDocuments")]
        public async Task<IActionResult> GetAllUsersKycDocuments()
        {
            try
            {
                var response = await _userKycDocumentManager.GetAllUsersKycDocuments();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
