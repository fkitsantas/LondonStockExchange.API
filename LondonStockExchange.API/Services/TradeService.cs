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
    public class TradeService : ITradeService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHubContext<TradeNotificationHub> _hubContext;
        private readonly IMapper _mapper;

        public TradeService(IStockRepository stockRepository, ITransactionRepository transactionRepository, IHubContext<TradeNotificationHub> hubContext, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _transactionRepository = transactionRepository;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public async Task<TradeResultDTO> ProcessTradeAsync(TradeDTO tradeDto)
        {
            var stock = await _stockRepository.GetByTickerSymbolAsync(tradeDto.TickerSymbol);
            if (stock == null)
            {
                return new TradeResultDTO { Success = false, Message = "Stock not found." };
            }

            stock.CurrentPrice = tradeDto.Price; // Update the stock price based on the trade
            await _stockRepository.UpdateAsync(stock);

            var transaction = _mapper.Map<Transaction>(tradeDto);
            transaction.StockID = stock.StockID;
            await _transactionRepository.AddAsync(transaction);

            // Notify all connected clients about the trade
            await _hubContext.Clients.All.SendAsync("ReceiveTradeInfo", tradeDto.TickerSymbol, tradeDto.Price, tradeDto.Shares, tradeDto.BrokerId);

            return new TradeResultDTO { Success = true, UpdatedStock = _mapper.Map<StockDTO>(stock) };
        }

        public async Task<IEnumerable<TransactionDTO>> GetRecentTransactionsByBrokerIdAsync(int brokerId, int limit)
        {
            var transactions = await _transactionRepository.GetRecentTransactionsAsync(limit);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public async Task<IEnumerable<TransactionDTO>> GetRecentTransactionsByStockIdAsync(string tickerSymbol, int limit)
        {
            var stock = await _stockRepository.GetByTickerSymbolAsync(tickerSymbol);
            if (stock == null)
            {
                return new List<TransactionDTO>(); 
            }

            var transactions = await _transactionRepository.GetAllByStockIdAsync(stock.StockID);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }
    }
}