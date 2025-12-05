using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly OLCConfig _olcConfig;
        public AccountController(IAuthenticateService authenticateService,
            INotyfService notyfService,
            IHttpContextAccessor httpContextAccessor,
            IOptions<OLCConfig> olcConfig)
        {
            _authenticateService = authenticateService;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;
            _olcConfig = olcConfig.Value;
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
            //this is the register mathod to register new users
            try
            {
                if (userRegistration != null)
                    userRegistration.RoleId = 2;

                var responce = await _authenticateService.RegisterUserAsync(userRegistration);

                if (responce.Success)
                    _notyfService.Success(responce.Message);
                else
                    _notyfService.Warning(responce.Message);

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

        [AllowAnonymous]
        public IActionResult ExternalLogin()
        {
            string returnUrl = _olcConfig.RedirectUri;

            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl,
                Items =
        {
            { "scheme", "Google" },
            { "returnUrl", returnUrl }
        }
            };

            return Challenge(props, "Google");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/", string remoteError = null)
        {
            try
            {
                ExternalUserInfo userinfo = new ExternalUserInfo();


                // Handle errors from the external provider
                if (remoteError != null)
                {
                    TempData["Error"] = $"Error from external provider: {remoteError}";
                    return RedirectToAction("Login");
                }

                // ✅ With your current setup, the user should already be authenticated
                if (User.Identity.IsAuthenticated)
                {
                    // User is already signed in automatically due to SignInScheme setting
                    userinfo.GivenName = User.FindFirst(ClaimTypes.GivenName)?.Value;
                    userinfo.Name = User.FindFirst(ClaimTypes.Name)?.Value;
                    userinfo.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                    userinfo.NameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    userinfo.Surname = User.FindFirst(ClaimTypes.Surname)?.Value;
                    _httpContextAccessor.HttpContext.Session.SetString("ExternalUserInfo", JsonConvert.SerializeObject(userinfo));
                    return View();
                }
                else
                {
                    // If not authenticated, try to authenticate with the default scheme
                    var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    if (authenticateResult?.Succeeded == true && authenticateResult.Principal != null)
                    {
                        // Sign in the user explicitly
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticateResult.Principal);

                        var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;

                        userinfo.Email = authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                        userinfo.GivenName = authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                        userinfo.Name = authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                        userinfo.NameIdentifier = authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        userinfo.Surname = authenticateResult.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                        _httpContextAccessor.HttpContext.Session.SetString("ExternalUserInfo", JsonConvert.SerializeObject(userinfo));
                        return View();
                    }
                    else
                    {
                        _notyfService.Error("An error occurred during login. Please try again.");
                        return RedirectToAction("Login");
                    }
                }
            }
            catch (Exception ex)
            {
                _notyfService.Error("An error occurred during login. Please try again.");
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExternalCallaBackResponse()
        {
            var externalUserInfo = _httpContextAccessor.HttpContext.Session.GetString("ExternalUserInfo");
            return Json(new { data = externalUserInfo });
        }

        [HttpPost]
        public async Task<IActionResult> LoginOrRegisterExternalUser([FromBody] ExternalUserInfo externalUserInfo)
        {

            try
            {
                var responce = await _authenticateService.LoginOrRegisterExternalUserAsync(externalUserInfo);
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
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public IActionResult ExternalLoginFailure()
        {
            return View();
        }

        // GET: /Account/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            try
            {
                var responce = await _authenticateService.ChangePasswordAsync(changePassword);
                if (responce)
                    _notyfService.Success("Successfully changed password");
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
