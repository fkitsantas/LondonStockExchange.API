using Microsoft.AspNetCore.Mvc;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<StocksController> _logger;

        public StocksController(IStockService stockService, ILogger<StocksController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        // GET: api/stocks
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

        // GET: api/stocks/{tickerSymbol}
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

        // GET: api/stocks/range?tickerSymbols=ABC,XYZ
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