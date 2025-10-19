using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        public UserController(IUserService userService,
            INotyfService notyfService)
        {
            _userService = userService;
            _notyfService = notyfService;
        }

        [Authorize(Roles = ("Administrator"))]
        public IActionResult Index()
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
    }
}
