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
        public async Task<IActionResult> GetAddressTypes([FromQuery]long Id) 
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
                    if (addressType.Id >0)
                      isSaved = await _addressTypeService.UpdateAddressTypeAsync(addressType);
                    else
                        isSaved = await _addressTypeService.InsertAddressTypeAsync(addressType);

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ActivateAddressType([FromBody] AddressType addressType)
        {
            try
            {
                bool isActivate = false;

                isActivate = await _addressTypeService.ActivateAddressTypeAsync(addressType);

                if (isActivate)
                    _notyfService.Success("Successfully activated address type");
                else
                    _notyfService.Error("Unable to activate address type");

                return Json(isActivate);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}