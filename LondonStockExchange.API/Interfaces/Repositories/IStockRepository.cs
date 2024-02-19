using LondonStockExchange.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Repositories
{
    /// <summary>
    /// Defines the contract for repository operations on the Stock entities.
    /// This interface abstracts the access and manipulation of stock data, allowing for a decoupled architecture
    /// and easier testing and maintenance.
    /// </summary>
    public interface IStockRepository
    {
        /// <summary>
        /// Asynchronously retrieves all stocks available in the repository.
        /// </summary>
        /// <returns>A task that upon completion contains an enumerable of all <see cref="Stock"/> entities.</returns>
        Task<IEnumerable<Stock>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves a stock by its unique ticker symbol.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol representing the stock.</param>
        /// <returns>A task that upon completion contains the <see cref="Stock"/> entity matching the ticker symbol, if found; otherwise, null.</returns>
        Task<Stock> GetByTickerSymbolAsync(string tickerSymbol);

        /// <summary>
        /// Asynchronously retrieves a stock by its unique identifier.
        /// </summary>
        /// <param name="stockId">The identifier of the stock.</param>
        /// <returns>A task that upon completion contains the <see cref="Stock"/> entity matching the identifier, if found; otherwise, null.</returns>
        Task<Stock> GetByIdAsync(int stockId);

        /// <summary>
        /// Asynchronously updates an existing stock in the repository.
        /// </summary>
        /// <param name="stock">The <see cref="Stock"/> entity to update.</param>
        /// <returns>A task that upon completion contains the updated <see cref="Stock"/> entity.</returns>
        Task<Stock> UpdateAsync(Stock stock);

        /// <summary>
        /// Asynchronously retrieves stocks by a collection of ticker symbols.
        /// </summary>
        /// <param name="tickerSymbols">An enumerable of ticker symbols representing the stocks to retrieve.</param>
        /// <returns>A task that upon completion contains an enumerable of <see cref="Stock"/> entities matching the given ticker symbols.</returns>
        Task<IEnumerable<Stock>> GetByTickerSymbolsAsync(IEnumerable<string> tickerSymbols);
    }
}