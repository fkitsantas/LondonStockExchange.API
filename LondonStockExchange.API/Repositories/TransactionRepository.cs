using LondonStockExchange.API.Data;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Repositories
{
    /// <summary>
    /// Handles data operations for transactions within the trading platform.
    /// Implements <see cref="ITransactionRepository"/> for transaction-specific operations.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for transaction operations.</param>
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously adds a new transaction to the database.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        /// <returns>The added transaction with its persistent state updated (e.g., with a new ID).</returns>
        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        /// <summary>
        /// Retrieves all transactions associated with a specific broker ID, asynchronously.
        /// </summary>
        /// <param name="brokerId">The identifier of the broker whose transactions are to be retrieved.</param>
        /// <returns>A list of transactions made by the specified broker.</returns>
        public async Task<IEnumerable<Transaction>> GetAllByBrokerIdAsync(int brokerId)
        {
            return await _context.Transactions.Where(t => t.BrokerID == brokerId).ToListAsync();
        }

        /// <summary>
        /// Retrieves all transactions associated with a specific stock ID, asynchronously.
        /// </summary>
        /// <param name="stockId">The identifier of the stock whose transactions are to be retrieved.</param>
        /// <returns>A list of transactions involving the specified stock.</returns>
        public async Task<IEnumerable<Transaction>> GetAllByStockIdAsync(int stockId)
        {
            return await _context.Transactions.Where(t => t.StockID == stockId).ToListAsync();
        }

        /// <summary>
        /// Retrieves a specified number of the most recent transactions, asynchronously.
        /// </summary>
        /// <param name="limit">The maximum number of transactions to retrieve.</param>
        /// <returns>A list of the most recent transactions up to the specified limit.</returns>
        public async Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int limit)
        {
            return await _context.Transactions.OrderByDescending(t => t.Timestamp).Take(limit).ToListAsync();
        }

        /// <summary>
        /// Retrieves transactions that occurred within a specified date range, asynchronously.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>A list of transactions that occurred within the specified date range.</returns>
        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions.Where(t => t.Timestamp >= startDate && t.Timestamp <= endDate).ToListAsync();
        }
    }
}