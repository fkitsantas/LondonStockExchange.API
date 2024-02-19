using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Services;
using LondonStockExchange.API.Controllers;
using System;
using System.Threading.Tasks;

namespace LondonStockExchange.UnitTests.Controllers
{
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

        [Fact]
        public async Task ProcessTrade_InvalidTrade_ReturnsBadRequest()
        {
            // Arrange
            var tradeDto = new TradeDTO(); // Assuming this is invalid due to missing required fields

            // No need to setup the mock since the controller should return before calling the service

            // Act
            var result = await _controller.ProcessTrade(tradeDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

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