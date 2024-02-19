using Microsoft.AspNetCore.Mvc;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        private readonly ILogger<TradesController> _logger;

        public TradesController(ITradeService tradeService, ILogger<TradesController> logger)
        {
            _tradeService = tradeService;
            _logger = logger;
        }

        // POST: api/trades
        [HttpPost]
        public async Task<ActionResult<TradeResultDTO>> ProcessTrade([FromBody] TradeDTO tradeDto)
        {
            if (tradeDto == null)
            {
                _logger.LogWarning("Attempted to process a null trade.");
                return BadRequest("Trade data must be provided.");
            }
            if (tradeDto.Shares <= 0)
            {
                _logger.LogWarning("Attempted to process a trade with invalid share count.");
                return BadRequest("Shares must be greater than zero.");
            }

            try
            {
                var tradeResult = await _tradeService.ProcessTradeAsync(tradeDto);
                return Ok(tradeResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the trade.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}