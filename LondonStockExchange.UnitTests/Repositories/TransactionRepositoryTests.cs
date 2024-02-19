using Xunit;
using Microsoft.EntityFrameworkCore;
using LondonStockExchange.API.Data;
using LondonStockExchange.API.Models;
using LondonStockExchange.API.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LondonStockExchange.UnitTests.Repositories
{
    public class TransactionRepositoryTests
    {
        private ApplicationDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name to ensure a fresh database
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            // Prepopulate the database
            if (!databaseContext.Stocks.Any())
            {
                databaseContext.Stocks.Add(new Stock { TickerSymbol = "AAPL", CurrentPrice = 150M });
                databaseContext.Stocks.Add(new Stock { TickerSymbol = "MSFT", CurrentPrice = 250M });
                databaseContext.SaveChanges();
            }

            if (!databaseContext.Brokers.Any())
            {
                databaseContext.Brokers.Add(new Broker { Name = "Broker A" });
                databaseContext.Brokers.Add(new Broker { Name = "Broker B" });
                databaseContext.SaveChanges();
            }

            return databaseContext;
        }

        [Fact]
        public async Task AddAsync_AddsTransactionSuccessfully()
        {
            using (var context = GetDatabaseContext())
            {
                var repository = new TransactionRepository(context);
                var transaction = new Transaction
                {
                    StockID = context.Stocks.First().StockID,
                    BrokerID = context.Brokers.First().BrokerID,
                    Price = 200M,
                    Shares = 10,
                    Timestamp = DateTime.UtcNow
                };

                // Act
                var result = await repository.AddAsync(transaction);

                // Assert
                Assert.NotNull(result);
                Assert.True(context.Transactions.Any(t => t.TransactionID == result.TransactionID));
            }
        }

        [Fact]
        public async Task GetAllByBrokerIdAsync_RetrievesTransactionsForBroker()
        {
            using (var context = GetDatabaseContext())
            {
                var repository = new TransactionRepository(context);
                var brokerId = context.Brokers.First().BrokerID;
                // Prepopulate with a transaction
                context.Transactions.Add(new Transaction
                {
                    StockID = context.Stocks.First().StockID,
                    BrokerID = brokerId,
                    Price = 200M,
                    Shares = 10,
                    Timestamp = DateTime.UtcNow
                });
                context.SaveChanges();

                // Act
                var transactions = await repository.GetAllByBrokerIdAsync(brokerId);

                // Assert
                Assert.NotEmpty(transactions);
                Assert.All(transactions, t => Assert.Equal(brokerId, t.BrokerID));
            }
        }

        [Fact]
        public async Task GetAllByStockIdAsync_RetrievesTransactionsForStock()
        {
            using (var context = GetDatabaseContext())
            {
                var repository = new TransactionRepository(context);
                var stockId = context.Stocks.First().StockID;
                context.Transactions.Add(new Transaction
                {
                    StockID = stockId,
                    BrokerID = context.Brokers.First().BrokerID,
                    Price = 200M,
                    Shares = 10,
                    Timestamp = DateTime.UtcNow
                });
                context.SaveChanges();

                // Act
                var transactions = await repository.GetAllByStockIdAsync(stockId);

                // Assert
                Assert.NotEmpty(transactions);
                Assert.All(transactions, t => Assert.Equal(stockId, t.StockID));
            }
        }

        [Fact]
        public async Task GetRecentTransactionsAsync_RetrievesRecentTransactions()
        {
            using (var context = GetDatabaseContext())
            {
                var repository = new TransactionRepository(context);
                // Ensure there are transactions to retrieve
                context.Transactions.AddRange(
                    new Transaction { StockID = context.Stocks.First().StockID, BrokerID = context.Brokers.First().BrokerID, Price = 200M, Shares = 10, Timestamp = DateTime.UtcNow.AddDays(-1) },
                    new Transaction { StockID = context.Stocks.First().StockID, BrokerID = context.Brokers.First().BrokerID, Price = 210M, Shares = 5, Timestamp = DateTime.UtcNow }
                );
                context.SaveChanges();

                // Act
                var transactions = await repository.GetRecentTransactionsAsync(2);

                // Assert
                Assert.Equal(2, transactions.Count());
                Assert.True(transactions.First().Timestamp >= transactions.Last().Timestamp);
            }
        }
    }
}
