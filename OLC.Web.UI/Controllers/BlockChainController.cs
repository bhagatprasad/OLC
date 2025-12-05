using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;
using System.Diagnostics.Eventing.Reader;

namespace OLC.Web.UI.Controllers
{
    [Authorize(Roles = "Administrator,Executive,User")]
    public class BlockChainController : Controller
    {
        private readonly IBlockChainService _blockChainService;
        private readonly INotyfService _notyfService;

        public BlockChainController(IBlockChainService blockChainService, INotyfService notyfService)
        {
            _blockChainService = blockChainService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> GetBlockChains()
        {
            try
            {
                var blockChainResponse = await _blockChainService.GetBlockChainsAsync();
                return Json(new { data = blockChainResponse });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public async Task <IActionResult> InsertBlockChainAsync([FromBody] BlockChain blockChain)
        {
            try
            {
                bool isSaved = false;

                if (blockChain != null)
                {
                    if (blockChain.Id > 0)
                        isSaved = await _blockChainService.UpdateBlockChainAsync(blockChain);
                    else
                        isSaved = await _blockChainService.InsertBlockChainAsync(blockChain);

                    _notyfService.Success("Successfully saved blockChain");

                    return Json(isSaved);
                }
                _notyfService.Error("Unable to save blockChain");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBlockChain(long id)
        {
            try
            {
                bool isSaved = false;
                if (id > 0)
                {
                    isSaved = await _blockChainService.DeleteBlockChainAsync(id);
                    if (isSaved)
                        _notyfService.Success("Succesfully Deleted BlockChain");
                    else
                        _notyfService.Warning("Unable to delete BlockChain");
                    return Json(isSaved);
                }
                _notyfService.Error("Unable to delete BlockChain");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
