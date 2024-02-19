using LondonStockExchange.API.Controllers;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Services;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace LondonStockExchange.UnitTests
{
    public class StocksControllerTests
    {
        [Fact]
        public async Task GetAllStocks_ReturnsExpectedStocks()
        {
            // Arrange
            var mockService = new Mock<IStockService>();
            var mockLogger = new Mock<ILogger<StocksController>>();
            mockService.Setup(service => service.GetAllStocksAsync())
                       .ReturnsAsync(new List<StockDTO> { new StockDTO { TickerSymbol = "AAPL", CurrentPrice = 150 } });
            var controller = new StocksController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.GetAllStocks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<StockDTO>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("AAPL", returnValue[0].TickerSymbol);
            Assert.Equal(150, returnValue[0].CurrentPrice);
        }

        [Fact]
        public async Task GetStockByTickerSymbol_ReturnsStock_WhenStockExists()
        {
            // Arrange
            var mockService = new Mock<IStockService>();
            var mockLogger = new Mock<ILogger<StocksController>>();
            var tickerSymbol = "AAPL";
            var expectedStock = new StockDTO { TickerSymbol = tickerSymbol, CurrentPrice = 150 };

            mockService.Setup(s => s.GetStockByTickerSymbolAsync(tickerSymbol)).ReturnsAsync(expectedStock);
            var controller = new StocksController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.GetStockByTickerSymbol(tickerSymbol);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var stock = Assert.IsType<StockDTO>(okResult.Value);
            Assert.Equal(expectedStock.TickerSymbol, stock.TickerSymbol);
            Assert.Equal(expectedStock.CurrentPrice, stock.CurrentPrice);
        }

        [Fact]
        public async Task GetStockByTickerSymbol_ReturnsNotFound_WhenStockDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IStockService>();
            var mockLogger = new Mock<ILogger<StocksController>>();
            var tickerSymbol = "AAPL";

            mockService.Setup(s => s.GetStockByTickerSymbolAsync(tickerSymbol)).ReturnsAsync((StockDTO)null);
            var controller = new StocksController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.GetStockByTickerSymbol(tickerSymbol);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetStocksByTickerSymbols_ReturnsStocks_WhenCalledWithValidSymbols()
        {
            // Arrange
            var mockService = new Mock<IStockService>();
            var mockLogger = new Mock<ILogger<StocksController>>();
            var tickerSymbols = new List<string> { "AAPL", "MSFT" };
            var expectedStocks = new List<StockDTO>
        {
            new StockDTO { TickerSymbol = "AAPL", CurrentPrice = 150 },
            new StockDTO { TickerSymbol = "MSFT", CurrentPrice = 290 }
        };

            mockService.Setup(s => s.GetStocksByTickerSymbolsAsync(tickerSymbols)).ReturnsAsync(expectedStocks);
            var controller = new StocksController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.GetStocksByTickerSymbols(tickerSymbols);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var stocks = Assert.IsType<List<StockDTO>>(okResult.Value);
            Assert.Equal(2, stocks.Count);
            Assert.Equal(tickerSymbols[0], stocks[0].TickerSymbol);
            Assert.Equal(tickerSymbols[1], stocks[1].TickerSymbol);
        }

        [Fact]
        public async Task GetAllStocks_ReturnsServerError_OnException()
        {
            // Arrange
            var mockService = new Mock<IStockService>();
            var mockLogger = new Mock<ILogger<StocksController>>();
            mockService.Setup(s => s.GetAllStocksAsync()).ThrowsAsync(new Exception("Test exception"));
            var controller = new StocksController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.GetAllStocks();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}