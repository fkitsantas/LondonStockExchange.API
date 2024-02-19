using LondonStockExchange.API.Models;
using System;
using System.Linq;

namespace LondonStockExchange.API.Data
{
    /// <summary>
    /// Provides functionality to seed the database with initial data.
    /// This static class is responsible for populating the ApplicationDbContext
    /// with predefined sets of brokers, stocks, and transactions if they do not already exist.
    /// It ensures that the application has a baseline dataset to work with upon startup.
    /// </summary>
    public static class DataSeeder
    {
        /// <summary>
        /// Seeds the ApplicationDbContext with initial data for brokers, stocks, and transactions.
        /// Checks each entity set for existing data to avoid duplication and ensures
        /// the database is created before seeding data.
        /// </summary>
        /// <param name="context">The application database context to seed data into.</param>
        public static void SeedData(ApplicationDbContext context)
        {
            // Ensure the database is created.
            context.Database.EnsureCreated();

            // Seed Brokers if none exist.
            if (!context.Brokers.Any())
            {
                context.Brokers.AddRange(new Broker[] {
                    new Broker { Name = "Broker A" },
                    new Broker { Name = "Broker B" },
                    new Broker { Name = "Broker C" }
                });
            }

            // Seed Stocks if none exist.
            if (!context.Stocks.Any())
            {
                context.Stocks.AddRange(new Stock[] {
                    new Stock { TickerSymbol = "AAPL", CurrentPrice = 150.00M },
                    new Stock { TickerSymbol = "MSFT", CurrentPrice = 250.00M },
                    new Stock { TickerSymbol = "GOOGL", CurrentPrice = 350.00M}
                });
            }

            // Seed Transactions if none exist.
            if (!context.Transactions.Any())
            {
                context.Transactions.AddRange(new Transaction[] {
                    new Transaction { BrokerID = 1, StockID = 1, Price = 150.00M, Shares = 10, Timestamp = DateTime.Now },
                    new Transaction { BrokerID = 2, StockID = 2, Price = 250.00M, Shares = 5, Timestamp = DateTime.Now.AddMinutes(-10) },
                    new Transaction { BrokerID = 3, StockID = 3, Price = 350.00M, Shares = 2, Timestamp = DateTime.Now.AddMinutes(-20) }
                });
            }

            // Commit the changes to the database.
            context.SaveChanges();
        }
    }
}