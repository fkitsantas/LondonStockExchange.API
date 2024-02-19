using AutoMapper;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using LondonStockExchange.API.Models;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Services;

namespace LondonStockExchange.UnitTests.Services
{
    public class StockServiceTests
    {
        private readonly Mock<IStockRepository> _mockStockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StockService _stockService;

        public StockServiceTests()
        {
            _mockStockRepository = new Mock<IStockRepository>();
            _mockMapper = new Mock<IMapper>();
            _stockService = new StockService(_mockStockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllStocksAsync_ReturnsAllStocks()
        {
            // Arrange
            var stocks = new List<Stock>
            {
                new Stock { StockID = 1, TickerSymbol = "AAPL", CurrentPrice = 150M },
                new Stock { StockID = 2, TickerSymbol = "MSFT", CurrentPrice = 200M }
            };
                    var stockDTOs = new List<StockDTO>
            {
                new StockDTO { TickerSymbol = "AAPL", CurrentPrice = 150M },
                new StockDTO { TickerSymbol = "MSFT", CurrentPrice = 200M }
            };

            _mockStockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(stocks);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<StockDTO>>(stocks)).Returns(stockDTOs);

            // Act
            var result = await _stockService.GetAllStocksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(stockDTOs.Count, result.Count());
            _mockStockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<StockDTO>>(stocks), Times.Once);
        }

        [Fact]
        public async Task GetStockByTickerSymbolAsync_ReturnsStock_WhenExists()
        {
            // Arrange
            var stock = new Stock { StockID = 1, TickerSymbol = "AAPL", CurrentPrice = 150M };
            var stockDTO = new StockDTO { TickerSymbol = "AAPL", CurrentPrice = 150M };

            _mockStockRepository.Setup(repo => repo.GetByTickerSymbolAsync("AAPL")).ReturnsAsync(stock);
            _mockMapper.Setup(mapper => mapper.Map<StockDTO>(stock)).Returns(stockDTO);

            // Act
            var result = await _stockService.GetStockByTickerSymbolAsync("AAPL");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(stockDTO.TickerSymbol, result.TickerSymbol);
            Assert.Equal(stockDTO.CurrentPrice, result.CurrentPrice);
            _mockStockRepository.Verify(repo => repo.GetByTickerSymbolAsync("AAPL"), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<StockDTO>(stock), Times.Once);
        }

        [Fact]
        public async Task UpdateStockPriceAsync_UpdatesPrice_WhenStockExists()
        {
            // Arrange
            var stock = new Stock { StockID = 1, TickerSymbol = "AAPL", CurrentPrice = 150M };
            var updatedStockDTO = new StockDTO { TickerSymbol = "AAPL", CurrentPrice = 160M };

            _mockStockRepository.Setup(repo => repo.GetByTickerSymbolAsync("AAPL")).ReturnsAsync(stock);
            _mockStockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Stock>())).ReturnsAsync(stock);
            _mockMapper.Setup(mapper => mapper.Map<StockDTO>(It.IsAny<Stock>())).Returns(updatedStockDTO);

            // Act
            var result = await _stockService.UpdateStockPriceAsync("AAPL", 160M);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedStockDTO.CurrentPrice, result.CurrentPrice);
            _mockStockRepository.Verify(repo => repo.GetByTickerSymbolAsync("AAPL"), Times.Once);
            _mockStockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Stock>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<StockDTO>(It.IsAny<Stock>()), Times.Once);
        }
    }
}
