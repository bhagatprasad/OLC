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
        public UserController(IUserService userService,
            INotyfService notyfService,
            IAuthenticateService authenticateService)
        {
            _userService = userService;
            _notyfService = notyfService;
            _authenticateService = authenticateService;
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
    }
}
