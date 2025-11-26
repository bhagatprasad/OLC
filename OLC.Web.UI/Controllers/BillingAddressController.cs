using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    public class BillingAddressController : Controller
    {
        private readonly IBillingAddressService _billingAddressService;
        private readonly INotyfService _notyfService;

        public BillingAddressController(IBillingAddressService billingAddressService,
            INotyfService notyfService)
        {
            _billingAddressService = billingAddressService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("User"))]
        public IActionResult UserBillingAddress()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive,User"))]
        public async Task<IActionResult> GetUserBillingAddresses(long userId)
        {
            try
            {
                var addresses = await _billingAddressService.GetUserBillingAddressesAsync(userId);
                return Json(new { data = addresses });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserBillingAddress(long billingAddressId)
        {
            try
            {
                bool isSaved = false;

                if (billingAddressId > 0)
                {
                    isSaved = await _billingAddressService.DeleteUserBillingAddressAsync(billingAddressId);

                    if (isSaved)
                        _notyfService.Success("Successfully deleted user billing address");
                    else
                        _notyfService.Warning("Unable to delete billing address");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to delete user billing address");

                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = ("User"))]
        public async Task<IActionResult> SaveUserBillingAddress([FromBody] UserBillingAddress userBillingAddress)
        {
            try
            {
                bool isSaved = false;

                if (userBillingAddress != null)
                {
                    if (userBillingAddress.Id > 0)
                        isSaved = await _billingAddressService.UpdateUserBillingAddressAsync(userBillingAddress);
                    else
                        isSaved = await _billingAddressService.InsertUserBillingAddressAsync(userBillingAddress);

                    _notyfService.Success("Successfully saved user billing address");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save user billing address");

                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<IActionResult> ActivateBillingAddress([FromBody] UserBillingAddress userBillingAddress)
        {
            try
            {
                bool isActivate = false;

                isActivate = await _billingAddressService.ActivateBillingAddress(userBillingAddress);

                if (isActivate)
                    _notyfService.Success("Successfully activated user Billing Address");
                else
                    _notyfService.Error("Unable to activate user Billing Address");

                return Json(isActivate);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
