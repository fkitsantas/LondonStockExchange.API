using LondonStockExchange.API.Data;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Repositories
{
    /// <summary>
    /// Repository for managing stock entities within the database.
    /// Implements the <see cref="IStockRepository"/> to provide concrete implementations of stock operations.
    /// </summary>
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockRepository"/> with the specified database context.
        /// </summary>
        /// <param name="context">The database context for data operations on stocks.</param>
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all stocks from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing a list of all stocks.</returns>
        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a stock by its ticker symbol.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock to find.</param>
        /// <returns>A task representing the asynchronous operation, containing the found stock or null if not found.</returns>
        public async Task<Stock> GetByTickerSymbolAsync(string tickerSymbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.TickerSymbol == tickerSymbol);
        }

        /// <summary>
        /// Asynchronously retrieves a stock by its unique identifier.
        /// </summary>
        /// <param name="stockId">The identifier of the stock to find.</param>
        /// <returns>A task representing the asynchronous operation, containing the found stock or null if not found.</returns>
        public async Task<Stock> GetByIdAsync(int stockId)
        {
            return await _context.Stocks.FindAsync(stockId);
        }

        /// <summary>
        /// Asynchronously updates a stock in the database.
        /// </summary>
        /// <param name="stock">The stock entity with updated information to save.</param>
        /// <returns>A task representing the asynchronous operation, containing the updated stock.</returns>
        public async Task<Stock> UpdateAsync(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return stock;
        }

        /// <summary>
        /// Asynchronously retrieves stocks by a collection of ticker symbols.
        /// </summary>
        /// <param name="tickerSymbols">The collection of ticker symbols to find matching stocks for.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of found stocks.</returns>
        public async Task<IEnumerable<Stock>> GetByTickerSymbolsAsync(IEnumerable<string> tickerSymbols)
        {
            return await _context.Stocks.Where(s => tickerSymbols.Contains(s.TickerSymbol)).ToListAsync();
        }
    }
}