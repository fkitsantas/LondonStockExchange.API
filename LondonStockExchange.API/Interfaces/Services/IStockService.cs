using LondonStockExchange.API.Models;
using LondonStockExchange.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Services
{
    /// <summary>
    /// Defines the service contract for managing stock information.
    /// This interface provides methods for retrieving and updating stock data, 
    /// encapsulating business logic associated with stock entities.
    /// </summary>
    public interface IStockService
    {
        /// <summary>
        /// Retrieves all available stocks in an asynchronous manner.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="StockDTO"/> representing all stocks.</returns>
        Task<IEnumerable<StockDTO>> GetAllStocksAsync();

        /// <summary>
        /// Retrieves a specific stock by its ticker symbol asynchronously.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="StockDTO"/> of the specified stock, if found; otherwise, null.</returns>
        Task<StockDTO> GetStockByTickerSymbolAsync(string tickerSymbol);

        /// <summary>
        /// Retrieves a collection of stocks by their ticker symbols asynchronously.
        /// </summary>
        /// <param name="tickerSymbols">An enumerable of ticker symbols representing the stocks to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="StockDTO"/> for the specified ticker symbols.</returns>
        Task<IEnumerable<StockDTO>> GetStocksByTickerSymbolsAsync(IEnumerable<string> tickerSymbols);

        /// <summary>
        /// Updates the price of a stock identified by its ticker symbol asynchronously.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock to update.</param>
        /// <param name="newPrice">The new price to set for the stock.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="StockDTO"/> with the new price.</returns>
        Task<StockDTO> UpdateStockPriceAsync(string tickerSymbol, decimal newPrice);
    }
}