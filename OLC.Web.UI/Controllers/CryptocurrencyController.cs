using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class CryptocurrencyController : Controller
    {
        private readonly ICryptocurrencyService _cryptocurrencyService;
        private readonly INotyfService _notyfService;

        public CryptocurrencyController(ICryptocurrencyService cryptocurrencyService, INotyfService notyfService)
        {
            _cryptocurrencyService = cryptocurrencyService;
            _notyfService = notyfService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> Index()
        {
            List<Cryptocurrency> cryptocurrencies = new List<Cryptocurrency>();

            var currencies = await _cryptocurrencyService.GetAllCryptocurrenciesAsync();

            if (currencies.Any())
            {
                cryptocurrencies = currencies;
            }

            return View(cryptocurrencies);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> GetAllCryptocurrencies()
        {
            try
            {
                var response = await _cryptocurrencyService.GetAllCryptocurrenciesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetAllCryptocurrencies(long id)
        {
            try
            {
                var response = await _cryptocurrencyService.GetCryptocurrencyByIdAsync(id);
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
        public async Task<IActionResult> SaveCryptocurrency([FromBody] Cryptocurrency cryptocurrency)
        {
            try
            {
                bool isSaved = false;

                if (cryptocurrency != null)
                {
                    if (cryptocurrency.Id > 0)
                        isSaved = await _cryptocurrencyService.UpdateCryptocurrencyAsync(cryptocurrency);
                    else
                        isSaved = await _cryptocurrencyService.InserCryptocurrencyAsync(cryptocurrency);

                    _notyfService.Success("Successfully saved Crypto currency");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to Crypto currency");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
