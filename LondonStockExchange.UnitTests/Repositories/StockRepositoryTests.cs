using Xunit;
using Microsoft.EntityFrameworkCore;
using LondonStockExchange.API.Data;
using LondonStockExchange.API.Models;
using LondonStockExchange.API.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LondonStockExchange.UnitTests.Repositories
{
    public class StockRepositoryTests
    {
        private ApplicationDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryStockDatabase")
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureDeleted();
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllStocks()
        {
            using (var context = GetDatabaseContext())
            {
                // Arrange
                var stockRepository = new StockRepository(context);
                context.Stocks.AddRange(new Stock { TickerSymbol = "AAPL", CurrentPrice = 150.00M },
                                        new Stock { TickerSymbol = "MSFT", CurrentPrice = 250.00M });
                context.SaveChanges();

                // Act
                var stocks = await stockRepository.GetAllAsync();

                // Assert
                Assert.Equal(2, stocks.Count());
            }
        }

        [Fact]
        public async Task GetByTickerSymbolAsync_ReturnsCorrectStock()
        {
            using (var context = GetDatabaseContext())
            {
                // Arrange
                var stockRepository = new StockRepository(context);
                var expectedStock = new Stock { TickerSymbol = "AAPL", CurrentPrice = 150.00M };
                context.Stocks.Add(expectedStock);
                context.SaveChanges();

                // Act
                var stock = await stockRepository.GetByTickerSymbolAsync("AAPL");

                // Assert
                Assert.NotNull(stock);
                Assert.Equal("AAPL", stock.TickerSymbol);
                Assert.Equal(150.00M, stock.CurrentPrice);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesStockCorrectly()
        {
            using (var context = GetDatabaseContext())
            {
                // Arrange
                var stockRepository = new StockRepository(context);
                var stock = new Stock { TickerSymbol = "AAPL", CurrentPrice = 150.00M };
                context.Stocks.Add(stock);
                context.SaveChanges();

                // Act
                stock.CurrentPrice = 200.00M;
                await stockRepository.UpdateAsync(stock);
                var updatedStock = await context.Stocks.FirstOrDefaultAsync(s => s.TickerSymbol == "AAPL");

                // Assert
                Assert.NotNull(updatedStock);
                Assert.Equal(200.00M, updatedStock.CurrentPrice);
            }
        }

        [Fact]
        public async Task GetByTickerSymbolsAsync_ReturnsCorrectStocks()
        {
            using (var context = GetDatabaseContext())
            {
                // Arrange
                var stockRepository = new StockRepository(context);
                context.Stocks.AddRange(new Stock { TickerSymbol = "AAPL", CurrentPrice = 150.00M },
                                        new Stock { TickerSymbol = "GOOGL", CurrentPrice = 250.00M },
                                        new Stock { TickerSymbol = "MSFT", CurrentPrice = 350.00M });
                context.SaveChanges();

                // Act
                var tickerSymbols = new List<string> { "AAPL", "GOOGL" };
                var stocks = await stockRepository.GetByTickerSymbolsAsync(tickerSymbols);

                // Assert
                Assert.Equal(2, stocks.Count());
                Assert.Contains(stocks, s => s.TickerSymbol == "AAPL");
                Assert.Contains(stocks, s => s.TickerSymbol == "GOOGL");
            }
        }
    }
}