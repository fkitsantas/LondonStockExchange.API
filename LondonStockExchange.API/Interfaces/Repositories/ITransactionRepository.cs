using LondonStockExchange.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Repositories
{
    /// <summary>
    /// Defines the contract for repository operations related to Transaction entities.
    /// This interface facilitates the abstraction of data access methods for transactions,
    /// promoting a clean separation of concerns and enhancing testability.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Adds a new transaction to the data store asynchronously.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        /// <returns>A task that represents the asynchronous add operation. The task result contains the added <see cref="Transaction"/>.</returns>
        Task<Transaction> AddAsync(Transaction transaction);

        /// <summary>
        /// Retrieves all transactions associated with a specific broker asynchronously.
        /// </summary>
        /// <param name="brokerId">The identifier of the broker.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="Transaction"/> entities related to the specified broker.</returns>
        Task<IEnumerable<Transaction>> GetAllByBrokerIdAsync(int brokerId);

        /// <summary>
        /// Retrieves all transactions associated with a specific stock asynchronously.
        /// </summary>
        /// <param name="stockId">The identifier of the stock.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="Transaction"/> entities related to the specified stock.</returns>
        Task<IEnumerable<Transaction>> GetAllByStockIdAsync(int stockId);

        /// <summary>
        /// Retrieves a limited number of the most recent transactions asynchronously.
        /// </summary>
        /// <param name="limit">The maximum number of transactions to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of the most recent <see cref="Transaction"/> entities up to the specified limit.</returns>
        Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int limit);

        /// <summary>
        /// Retrieves transactions that occurred within a specific date range asynchronously.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="Transaction"/> entities that occurred within the specified date range.</returns>
        Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}