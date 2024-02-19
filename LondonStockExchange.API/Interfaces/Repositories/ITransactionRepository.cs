using LondonStockExchange.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllByBrokerIdAsync(int brokerId);
        Task<IEnumerable<Transaction>> GetAllByStockIdAsync(int stockId);
        Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int limit);
        Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);

    }
}