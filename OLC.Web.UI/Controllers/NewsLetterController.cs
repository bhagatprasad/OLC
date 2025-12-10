using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    public class NewsLetterController : Controller
    {
        private readonly INewsLetterService _newsLetterService;
        private readonly INotyfService _notyfService;
        public NewsLetterController(INewsLetterService newsLetterService, INotyfService notyfService)
        {
            _newsLetterService = newsLetterService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetNewsLetters()
        {
            try
            {
                var response = await _newsLetterService.GetNewsLettersAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task <IActionResult> InsertNewsLetter([FromBody] NewsLetter newsLetter)
        {
            try
            {
                bool isSaved = false;

                if (newsLetter != null)
                {
                        isSaved = await _newsLetterService.InsertNewsLetterAsync(newsLetter);

                       _notyfService.Success("Thanks for subscring our newsletter , will get back to you with latest offers and promations");

                    return Json(isSaved);
                }
                _notyfService.Error("Unable to save news Letter");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }   
    }
}
