using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class AddressTypeController : Controller
    {
        private readonly IAddressTypeService _addressTypeService;
        private readonly INotyfService _notyfService;

        public AddressTypeController(IAddressTypeService addressTypeService, INotyfService notyfService)
        {
            _addressTypeService = addressTypeService;
            _notyfService = notyfService;
        }

        [HttpGet("/AddressType")]
        [Authorize(Roles = "Administrator,Executive")]
        public IActionResult AddressTypes()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetAddressTypes([FromQuery] long Id)  // Fixed: Accept Id parameter
        {
            try
            {
                var response = await _addressTypeService.GetAddressTypeAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveAddressType([FromBody] AddressType addressType)
        {
            try
            {
                bool isSaved = false;

                if (addressType != null)
                {
                    // Call a single service method
                    isSaved = await _addressTypeService.SaveAddressTypeAsync(addressType);

                    _notyfService.Success("Successfully saved address type");
                    return Json(isSaved);
                }

                _notyfService.Error("Unable to save address type");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAddressType(long addressTypeId)
        {
            try
            {
                bool isSaved = false;
                if (addressTypeId > 0)
                {
                    isSaved = await _addressTypeService.DeleteAddressTypeAsync(addressTypeId);

                    if (isSaved)
                        _notyfService.Success("Successfully deleted address type");
                    else
                        _notyfService.Warning("Unable to delete address type");

                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete address type");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}