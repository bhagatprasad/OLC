using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;
using System.Linq.Expressions;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockChainController : ControllerBase
    {
        private readonly IBlockChainManager _blockChainManager;

        public BlockChainController(IBlockChainManager blockChainManager)
        {
            _blockChainManager = blockChainManager;
        }

        [HttpGet]
        [Route("GetBlockChainsAsync")]
        public async Task<IActionResult> GetBlockChainsAsync()
        {
            try
            {
                var response = await _blockChainManager.GetBlockChainsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetBlockChainByIdAsync/{id}")]
        public async Task<IActionResult> GetBlockChainByIdAsync(long id)
        {
            try
            {
                var response = await _blockChainManager.GetBlockChainByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertBlockChainAsync")]
        public async Task<IActionResult> InsertBlockChainAsync(BlockChain blockChain)
        {
            try
            {
                var response = await _blockChainManager.InsertBlockChainAsync(blockChain);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateBlockChainAsync")]
        public async Task<IActionResult> UpdateBlockChainAsync(BlockChain blockChain)
        {
            try
            {
                var response = await _blockChainManager.UpdateBlockChainAsync(blockChain);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteBlockChainAsync/{id}")]
        public async Task<IActionResult> DeleteBlockChainAsync(long id)
        {
            try
            {
                var response = await _blockChainManager.DeleteBlockChainAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
