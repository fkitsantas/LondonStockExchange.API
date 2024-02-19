using LondonStockExchange.API.Data;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock> GetByTickerSymbolAsync(string tickerSymbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.TickerSymbol == tickerSymbol);
        }

        public async Task<Stock> GetByIdAsync(int stockId)
        {
            return await _context.Stocks.FindAsync(stockId);
        }

        public async Task<Stock> UpdateAsync(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<IEnumerable<Stock>> GetByTickerSymbolsAsync(IEnumerable<string> tickerSymbols)
        {
            return await _context.Stocks.Where(s => tickerSymbols.Contains(s.TickerSymbol)).ToListAsync();
        }
    }
}