using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IUserKycManager _userKycManager;
        private readonly IUserKycDocumentManager _userKycDocumentManager;
        public UserController(IUserManager userManager, IUserKycManager userKycManager, IUserKycDocumentManager userKycDocumentManager)
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
        [HttpGet]
        [Route("GetUserAccountAsync/{userId}")]
        public async Task<IActionResult> GetUserAccountAsync(long userId)
        {
            try
            {
                var response = await _userManager.GetUserAccountAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("InsertUserKycAsync")]
        public async Task<IActionResult> InsertUserKycAsync(UserKyc userKyc)
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
        [Route("UploadeUserKycDocumentAsync")]
        public async Task<IActionResult> UploadeUserKycDocumentAsync(UserKycDocument userKycDocument)
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
        [Route("UpdateUserKycDocumentAsync")]
        public async Task<IActionResult> UpdateUserKycDocumentAsync(UserKycDocument userKycDocument)
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
                var response = await _userKycDocumentManager.GetAllUsersKycDocumentsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("PreviewUserKycDocumentAsync/{userId}")]
        public async Task<IActionResult> PreviewUserKycDocumentAsync(long userId)
        {
            try
            {
                PreviewUserKycDocument previewUserKycDocument = new PreviewUserKycDocument();

                var userKyc = await _userKycManager.GetUserKycByUserIdAsync(userId);

                var userKycDocument = await _userKycDocumentManager.GetUserKycDocumentByUserAsync(userId);

                if (userKyc != null)
                    previewUserKycDocument.userKyc = userKyc;
                if (userKycDocument != null)
                    previewUserKycDocument.userKycDocument = userKycDocument;

                return Ok(previewUserKycDocument);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("VerifyUserDocumentKyc")]
        public async Task<IActionResult> VerifyUserDocumentKycAsync(VerifyUserKyc verifyUserKyc)
        {
            try
            {
                var response = await _userKycManager.VerifyUserKycAsync(verifyUserKyc);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateUserPersonalInformationAsync")]
        public async Task<IActionResult> UpdateUserPersonalInformationAsync(UserPersonalInformation userPersonalInformation)
        {
            try
            {
                var response = await _userManager.UpdateUserPersonalInformationAsync(userPersonalInformation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
