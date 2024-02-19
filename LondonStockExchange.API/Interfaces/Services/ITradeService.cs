using LondonStockExchange.API.Models;
using LondonStockExchange.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Services
{
    /// <summary>
    /// Defines the service contract for executing and managing trade transactions.
    /// This interface encapsulates the business logic necessary for processing trades,
    /// retrieving recent transactions, and facilitating real-time data flow within the trading platform.
    /// </summary>
    public interface ITradeService
    {
        /// <summary>
        /// Processes a trade asynchronously based on the provided trade details.
        /// </summary>
        /// <param name="tradeDto">The data transfer object containing trade details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a <see cref="TradeResultDTO"/> indicating the outcome of the trade processing.</returns>
        Task<TradeResultDTO> ProcessTradeAsync(TradeDTO tradeDto);

        /// <summary>
        /// Retrieves a collection of the most recent transactions made by a specific broker, limited by a specified count, asynchronously.
        /// </summary>
        /// <param name="brokerId">The identifier of the broker whose transactions are to be retrieved.</param>
        /// <param name="limit">The maximum number of transactions to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="TransactionDTO"/> limited to the specified count.</returns>
        Task<IEnumerable<TransactionDTO>> GetRecentTransactionsByBrokerIdAsync(int brokerId, int limit);

        /// <summary>
        /// Retrieves a collection of the most recent transactions for a specific stock, limited by a specified count, asynchronously.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock whose transactions are to be retrieved.</param>
        /// <param name="limit">The maximum number of transactions to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="TransactionDTO"/> for the specified stock, limited to the specified count.</returns>
        Task<IEnumerable<TransactionDTO>> GetRecentTransactionsByStockIdAsync(string tickerSymbol, int limit);
    }
}