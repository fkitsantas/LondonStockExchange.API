using LondonStockExchange.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock> GetByTickerSymbolAsync(string tickerSymbol);
        Task<Stock> GetByIdAsync(int stockId);
        Task<Stock> UpdateAsync(Stock stock);
        Task<IEnumerable<Stock>> GetByTickerSymbolsAsync(IEnumerable<string> tickerSymbols);
    }
}