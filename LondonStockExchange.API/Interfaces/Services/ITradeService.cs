using LondonStockExchange.API.Models;
using LondonStockExchange.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Services
{
    public interface ITradeService
    {
        Task<TradeResultDTO> ProcessTradeAsync(TradeDTO tradeDto);
        Task<IEnumerable<TransactionDTO>> GetRecentTransactionsByBrokerIdAsync(int brokerId, int limit);
        Task<IEnumerable<TransactionDTO>> GetRecentTransactionsByStockIdAsync(string tickerSymbol, int limit);
    }
}
