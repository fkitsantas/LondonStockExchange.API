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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllByBrokerIdAsync(int brokerId)
        {
            return await _context.Transactions.Where(t => t.BrokerID == brokerId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllByStockIdAsync(int stockId)
        {
            return await _context.Transactions.Where(t => t.StockID == stockId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int limit)
        {
            return await _context.Transactions.OrderByDescending(t => t.Timestamp).Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions.Where(t => t.Timestamp >= startDate && t.Timestamp <= endDate).ToListAsync();
        }
    }
}