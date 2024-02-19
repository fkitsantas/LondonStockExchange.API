using LondonStockExchange.API.Models;
using LondonStockExchange.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Services
{
    public interface IStockService
    {
        Task<IEnumerable<StockDTO>> GetAllStocksAsync();
        Task<StockDTO> GetStockByTickerSymbolAsync(string tickerSymbol);
        Task<IEnumerable<StockDTO>> GetStocksByTickerSymbolsAsync(IEnumerable<string> tickerSymbols);
        Task<StockDTO> UpdateStockPriceAsync(string tickerSymbol, decimal newPrice);
    }
}
