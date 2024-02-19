using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Services;
using LondonStockExchange.API.Controllers;
using System.Threading.Tasks;

namespace LondonStockExchange.UnitTests.Controllers
{
    /// <summary>
    /// Contains unit tests for the TradesController class, ensuring all endpoints behave correctly under various scenarios.
    /// </summary>
    public class TradesControllerTests
    {
        private readonly Mock<ITradeService> _mockTradeService;
        private readonly Mock<ILogger<TradesController>> _mockLogger;
        private readonly TradesController _controller;

        public TradesControllerTests()
        {
            _mockTradeService = new Mock<ITradeService>();
            _mockLogger = new Mock<ILogger<TradesController>>();
            _controller = new TradesController(_mockTradeService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests that ProcessTrade action correctly processes a valid trade and returns an Ok response.
        /// </summary>
        [Fact]
        public async Task ProcessTrade_ValidTrade_ReturnsOk()
        {
            // Arrange
            var tradeDto = new TradeDTO { TickerSymbol = "AAPL", Price = 150.00m, Shares = 10, BrokerId = 1 };
            var tradeResultDto = new TradeResultDTO { Success = true, Message = "Trade processed successfully", UpdatedStock = new StockDTO { TickerSymbol = "AAPL", CurrentPrice = 150.00m } };

            _mockTradeService.Setup(s => s.ProcessTradeAsync(tradeDto)).ReturnsAsync(tradeResultDto);

            // Act
            var result = await _controller.ProcessTrade(tradeDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TradeResultDTO>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Trade processed successfully", returnValue.Message);
        }

        /// <summary>
        /// Tests that ProcessTrade action returns a BadRequest response when provided with an invalid trade.
        /// </summary>
        [Fact]
        public async Task ProcessTrade_InvalidTrade_ReturnsBadRequest()
        {
            // Arrange
            var tradeDto = new TradeDTO(); // Invalid trade due to missing required fields

            // Act
            var result = await _controller.ProcessTrade(tradeDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        /// <summary>
        /// Tests that ProcessTrade action returns an InternalServerError response when the service throws an exception.
        /// </summary>
        [Fact]
        public async Task ProcessTrade_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var tradeDto = new TradeDTO { TickerSymbol = "AAPL", Price = 150.00m, Shares = 10, BrokerId = 1 };
            _mockTradeService.Setup(s => s.ProcessTradeAsync(tradeDto)).ThrowsAsync(new Exception("Internal service error"));

            // Act
            var result = await _controller.ProcessTrade(tradeDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}