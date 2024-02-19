using Microsoft.AspNetCore.Mvc;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to stock information, providing endpoints for retrieving stock data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<StocksController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StocksController"/> class.
        /// </summary>
        /// <param name="stockService">The service for accessing stock data.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        public StocksController(IStockService stockService, ILogger<StocksController> logger)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all stocks available in the system.
        /// </summary>
        /// <returns>A collection of <see cref="StockDTO"/> instances.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDTO>>> GetAllStocks()
        {
            try
            {
                var stocks = await _stockService.GetAllStocksAsync();
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all stocks.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves a specific stock by its ticker symbol.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock to retrieve.</param>
        /// <returns>A <see cref="StockDTO"/> instance if found; otherwise, a NotFound result.</returns>
        [HttpGet("{tickerSymbol}")]
        public async Task<ActionResult<StockDTO>> GetStockByTickerSymbol(string tickerSymbol)
        {
            if (string.IsNullOrWhiteSpace(tickerSymbol))
            {
                _logger.LogWarning("GetStockByTickerSymbol was called with null or empty tickerSymbol.");
                return BadRequest("Ticker symbol must be provided.");
            }

            try
            {
                var stock = await _stockService.GetStockByTickerSymbolAsync(tickerSymbol);
                if (stock == null)
                {
                    _logger.LogInformation($"Stock with ticker symbol {tickerSymbol} not found.");
                    return NotFound();
                }
                return Ok(stock);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get stock by ticker symbol: {tickerSymbol}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves stocks for a given set of ticker symbols.
        /// </summary>
        /// <param name="tickerSymbols">The collection of ticker symbols to retrieve stocks for.</param>
        /// <returns>A collection of <see cref="StockDTO"/> instances.</returns>
        [HttpGet("range")]
        public async Task<ActionResult<IEnumerable<StockDTO>>> GetStocksByTickerSymbols([FromQuery] IEnumerable<string> tickerSymbols)
        {
            if (tickerSymbols == null || !tickerSymbols.Any())
            {
                _logger.LogWarning("GetStocksByTickerSymbols was called without ticker symbols.");
                return BadRequest("At least one ticker symbol must be provided.");
            }

            try
            {
                var stocks = await _stockService.GetStocksByTickerSymbolsAsync(tickerSymbols);
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get stocks by ticker symbols.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}