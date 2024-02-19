using AutoMapper;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Hubs;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Interfaces.Services;
using LondonStockExchange.API.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Services
{
    /// <summary>
    /// Service handling trade operations including processing trades and notifying about trade updates.
    /// </summary>
    public class TradeService : ITradeService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHubContext<TradeNotificationHub> _hubContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeService"/> class with required repositories, SignalR hub context, and AutoMapper.
        /// </summary>
        /// <param name="stockRepository">The repository for stock-related operations.</param>
        /// <param name="transactionRepository">The repository for transaction-related operations.</param>
        /// <param name="hubContext">The SignalR hub context for real-time notifications.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public TradeService(IStockRepository stockRepository, ITransactionRepository transactionRepository, IHubContext<TradeNotificationHub> hubContext, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _transactionRepository = transactionRepository;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Processes a trade, updates the stock price, records the transaction, and notifies all connected clients via SignalR.
        /// </summary>
        /// <param name="tradeDto">The trade data transfer object containing the trade details.</param>
        /// <returns>A task representing the asynchronous operation, containing the trade result including success status and updated stock information.</returns>
        public async Task<TradeResultDTO> ProcessTradeAsync(TradeDTO tradeDto)
        {
            var stock = await _stockRepository.GetByTickerSymbolAsync(tradeDto.TickerSymbol);
            if (stock == null)
            {
                return new TradeResultDTO { Success = false, Message = "Stock not found." };
            }

            stock.CurrentPrice = tradeDto.Price; // Update the stock price based on the trade
            await _stockRepository.UpdateAsync(stock);

            var transaction = _mapper.Map<Transaction>(tradeDto); // Map DTO to Transaction model
            transaction.StockID = stock.StockID;
            await _transactionRepository.AddAsync(transaction);

            // Notify all connected clients about the new trade
            await _hubContext.Clients.All.SendAsync("ReceiveTradeInfo", tradeDto.TickerSymbol, tradeDto.Price, tradeDto.Shares, tradeDto.BrokerId);

            return new TradeResultDTO { Success = true, UpdatedStock = _mapper.Map<StockDTO>(stock) };
        }

        /// <summary>
        /// Retrieves recent transactions made by a specific broker.
        /// </summary>
        /// <param name="brokerId">The broker's identifier.</param>
        /// <param name="limit">The maximum number of transactions to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of transactions.</returns>
        public async Task<IEnumerable<TransactionDTO>> GetRecentTransactionsByBrokerIdAsync(int brokerId, int limit)
        {
            var transactions = await _transactionRepository.GetRecentTransactionsAsync(limit);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        /// <summary>
        /// Retrieves recent transactions for a specific stock.
        /// </summary>
        /// <param name="tickerSymbol">The stock's ticker symbol.</param>
        /// <param name="limit">The maximum number of transactions to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of transactions.</returns>
        public async Task<IEnumerable<TransactionDTO>> GetRecentTransactionsByStockIdAsync(string tickerSymbol, int limit)
        {
            var stock = await _stockRepository.GetByTickerSymbolAsync(tickerSymbol);
            if (stock == null)
            {
                return new List<TransactionDTO>(); // Return an empty list if the stock is not found
            }

            var transactions = await _transactionRepository.GetAllByStockIdAsync(stock.StockID);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }
    }
}