using LondonStockExchange.API.Models;

namespace LondonStockExchange.API.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Ensure the database exists and is created
            context.Database.EnsureCreated();

            // Check if there are any brokers already, if not, seed them
            if (!context.Brokers.Any())
            {
                context.Brokers.AddRange(new Broker[] {
                new Broker { Name = "Broker A" },
                new Broker { Name = "Broker B" },
                new Broker { Name = "Broker C" }
            });
            }

            // Check if there are any stocks already, if not, seed them
            if (!context.Stocks.Any())
            {
                context.Stocks.AddRange(new Stock[] {
                new Stock { TickerSymbol = "AAPL", CurrentPrice = 150.00M },
                new Stock { TickerSymbol = "MSFT", CurrentPrice = 250.00M },
                new Stock { TickerSymbol = "GOOGL", CurrentPrice = 350.00M}
            });
            }

            // Check if there are any transactions already, if not, seed them
            if (!context.Transactions.Any())
            {
                context.Transactions.AddRange(new Transaction[] {
                new Transaction { BrokerID = 1, StockID = 1, Price = 150.00M, Shares = 10, Timestamp = DateTime.Now },
                new Transaction { BrokerID = 2, StockID = 2, Price = 250.00M, Shares = 5, Timestamp = DateTime.Now.AddMinutes(-10) },
                new Transaction { BrokerID = 3, StockID = 3, Price = 350.00M, Shares = 2, Timestamp = DateTime.Now.AddMinutes(-20) }
            });
            }

            context.SaveChanges();
        }
    }
}