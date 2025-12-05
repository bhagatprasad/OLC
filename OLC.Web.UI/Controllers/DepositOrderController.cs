using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class DepositOrderController : Controller
    {
        private readonly IDepositOrdereService _depositservice;
        private readonly INotyfService _notyfService;

        public DepositOrderController(IDepositOrdereService depositservice, INotyfService notyfService)
        {
            _depositservice = depositservice;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetDepositOrders(long paymentOrderId)
        {
            try
            {
                var response = await _depositservice.GetDepositOrderByOrderIdAsync(paymentOrderId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetAllDepositOrders()
        {
            try
            {
                var response = await _depositservice.GetAllDepositOrdersAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> InsertDepositOrder(DepositOrder depositOrder)
        {

            try
            {
                bool isSaved = false;

                if (depositOrder != null)
                {

                    isSaved = await _depositservice.InsertDepositOrderAsync(depositOrder);

                    _notyfService.Success("Successfully inserted deposit order");
                    return Json(isSaved);
                }

                _notyfService.Error("Unable to insert deposit order");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetDepositOrderByUserId(long userId)
        {
            try
            {
                var response = await _depositservice.GetDepositOrderByUserIdAsync(userId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Executive")]
        public async Task<IActionResult> uspGetAllExecutiveDepositOrderDetails()
        {
            try
            {
                var response = await _depositservice.uspGetAllExecutiveDepositOrderDetailsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
