using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize]
    [Authorize(Roles = ("Administrator,Executive,User"))]
    public class PaymentOrderController :Controller
    {
        private readonly IPaymentOrderService _paymentOrderService;
        private readonly INotyfService _notyfService;
        public PaymentOrderController(IPaymentOrderService paymentOrderService,INotyfService notyfService)
        {
            _paymentOrderService = paymentOrderService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetPaymentOrdersByUserId(long userId)
        {
            try
            {
                var response = await _paymentOrderService.GetPaymentOrdersByUserIdAsync(userId);
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator, Executive")]
        public async Task<IActionResult> GetAllPaymentOrders()
        {
            try
            {
                var response = await _paymentOrderService.GetPaymentOrdersAsync();
                return Json(new {data =response});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> SavePaymentOrder([FromBody] PaymentOrder paymentOrder)
        {
            try
            {
                bool isSuccess = false;

                isSuccess = await _paymentOrderService.InsertPaymentOrderAsync(paymentOrder);
                if(isSuccess)
                {
                    _notyfService.Success("Save operation sucessfull");
                    return Json(true);
                }
                else
                {
                    _notyfService.Success("Save operation unsucessfull");
                    return Json(true);
                }              
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
