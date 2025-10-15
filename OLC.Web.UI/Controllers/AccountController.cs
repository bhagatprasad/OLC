using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLC.Web.UI.Helper;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using System.Numerics;
using System.Security.Claims;

namespace OLC.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IAuthenticateService authenticateService,
            INotyfService notyfService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authenticateService = authenticateService;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Clear session data
                HttpContext.Session.Clear();

                // Clear all cookies
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                // Sign out the user
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            }

            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserRegistration userRegistration)
        {
            try
            {
                if (userRegistration != null)
                    userRegistration.RoleId = 2;

                var responce = await _authenticateService.RegisterUserAsync(userRegistration);

                if (responce)
                    _notyfService.Success("User registration successfull");
                else
                    _notyfService.Warning("User registration unsuccessfull");

                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }


        [HttpPost]
        public async Task<JsonResult> Login([FromBody] UserAuthentication authentication)
        {
            try
            {
                var responce = await _authenticateService.AuthenticateUserAsync(authentication);
                //check the response here

                if (responce != null)
                {
                    if (!string.IsNullOrEmpty(responce.Email))
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("AccessToken", responce.Email);

                        var userClaimes = await _authenticateService.GenarateUserClaimsAsync(responce);

                        _httpContextAccessor.HttpContext.Session.SetString("ApplicationUser", JsonConvert.SerializeObject(userClaimes));

                        var claimsIdentity = UserPrincipal.GenarateUserPrincipal(userClaimes);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                           new ClaimsPrincipal(claimsIdentity),
                                                           new AuthenticationProperties
                                                           {
                                                               IsPersistent = true,
                                                               ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                                                           });
                        return Json(new { appUser = userClaimes });
                    }

                    _notyfService.Error(responce.StatusMessage);
                }
                else
                {
                    _notyfService.Error("Something went wrong");
                }

                return Json(new { appUser = default(object) });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }

        }

        public async Task<IActionResult> Logout()
        {
            var appuser = _httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");
            HttpContext.Session.Remove("AccessToken");
            HttpContext.Session.Remove("ApplicationUser");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("allowCookies");
            return RedirectToAction("Login", "Account", null);
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            try
            {
                var responce = await _authenticateService.ForgotPasswordAsync(forgotPassword);
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(long userId)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            try
            {
                var responce = await _authenticateService.ResetPasswordAsync(resetPassword);
                if (responce)
                    _notyfService.Success("Successfully reseted password, please login");
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
