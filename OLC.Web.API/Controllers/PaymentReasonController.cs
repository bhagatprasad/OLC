using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentReasonController : ControllerBase
    {
        private readonly IPaymentReasonManager _paymentReasonManager;
        public PaymentReasonController(IPaymentReasonManager paymentReasonManager)
        {
            _paymentReasonManager = paymentReasonManager;
        }
        [HttpGet]
        [Route("GetPaymentReasonsAsync")]
        public async Task<IActionResult> GetPaymentReasonsAsync()
        {
            try
            {
                var response = await _paymentReasonManager.GetPaymentReasonsAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
