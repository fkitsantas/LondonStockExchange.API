using Microsoft.AspNetCore.Mvc;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling trade-related actions.
    /// This includes processing new trades and managing trade data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        private readonly ILogger<TradesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TradesController"/> class.
        /// </summary>
        /// <param name="tradeService">The service handling trade operations.</param>
        /// <param name="logger">The logger for capturing log information.</param>
        public TradesController(ITradeService tradeService, ILogger<TradesController> logger)
        {
            _tradeService = tradeService ?? throw new ArgumentNullException(nameof(tradeService), "Trade service cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        /// <summary>
        /// Processes a new trade.
        /// Validates the input trade data and, if valid, proceeds to process the trade.
        /// </summary>
        /// <param name="tradeDto">The trade data to process.</param>
        /// <returns>An <see cref="ActionResult"/> containing the result of the trade operation.</returns>
        /// <remarks>
        /// The method ensures the trade data is not null and the share count is greater than zero before processing.
        /// It handles any exceptions during the trade processing by logging the error and returning an appropriate response.
        /// </remarks>
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