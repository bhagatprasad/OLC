using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive"))]
    public class AddressTypeController : Controller
    {
        private readonly IAddressTypeService _addressTypeService;
        private readonly INotyfService _notyfService;

        public AddressTypeController(IAddressTypeService addressTypeService,
            INotyfService notyfService)
        {
            _addressTypeService = addressTypeService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAddressTypes()
        {
            try
            {
                var response = await _addressTypeService.GetAddressTypeAsync();
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
        public async Task<IActionResult> InsertOrUpdateAddressType([FromBody] AddressType addressType)
        {
            try
            {
                bool isSucess = false;

                if (addressType.Id > 0)
                    isSucess = await _addressTypeService.InsertAddressTypeAsync(addressType);
                else
                    isSucess = await _addressTypeService.UpdateAddressTypeAsync(addressType);

                _notyfService.Success("Save operation successful");

                return Json(true);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }

}
