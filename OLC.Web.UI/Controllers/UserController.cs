using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        private readonly IAuthenticateService _authenticateService;
        private readonly IUserKycService _userKycService;
        public UserController(IUserService userService,
            INotyfService notyfService,
            IAuthenticateService authenticateService,
            IUserKycService userKycService )
        {
            _userService = userService;
            _notyfService = notyfService;
            _authenticateService = authenticateService;
            _userKycService = userKycService;
        }

        [Authorize(Roles = ("Administrator"))]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult ManageUser(long userId, bool isReadOnly)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> GetUserAccounts()
        {
            try
            {
                var response = await _userService.GetUserAccountsAsync();
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> CreatePortalUser([FromBody] UserRegistration userRegistration)
        {
            try
            {
                var response = await _authenticateService.RegisterUserAsync(userRegistration);
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetUserKycByUSerId(long userId)
        {
            try
            {
                var response = await _userKycService.GetUserKycByUserIdAsync(userId);
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAllUsersKyc( )
        {
            try
            {
                var response = await _userKycService.GetAllUsersKycAsync();
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAllUsersKyc(UserKyc userKyc)
        {
            try
            {
                var response = await _userKycService.InsertUserKycAsync(userKyc);
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertAndUpdateUserKyc([FromBody] UserKyc userKyc)
        {
            try
            {
                bool isSaved = false;

                if (userKyc != null)
                {
                    if (userKyc.Id > 0)
                        isSaved = await _userKycService.ProcessUserKycAsync(userKyc);
                    else
                        isSaved = await _userKycService.InsertUserKycAsync(userKyc);

                    _notyfService.Success("Successfully Inserted User Kyc");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to Insert User Kyc");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
