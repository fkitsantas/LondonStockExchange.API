using AutoMapper;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Services
{
    /// <summary>
    /// Provides services for managing stock data, including retrieval and updates.
    /// Implements the <see cref="IStockService"/> interface.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class with specified stock repository and mapper.
        /// </summary>
        /// <param name="stockRepository">The repository for stock data operations.</param>
        /// <param name="mapper">The AutoMapper instance for object-to-object mapping.</param>
        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously retrieves all stocks and maps them to their DTO representation.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing a list of <see cref="StockDTO"/>.</returns>
        public async Task<IEnumerable<StockDTO>> GetAllStocksAsync()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StockDTO>>(stocks);
        }

        /// <summary>
        /// Asynchronously retrieves a single stock by its ticker symbol and maps it to its DTO representation.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock.</param>
        /// <returns>A task representing the asynchronous operation, containing the mapped <see cref="StockDTO"/> or null if not found.</returns>
        public async Task<StockDTO> GetStockByTickerSymbolAsync(string tickerSymbol)
        {
            var stock = await _stockRepository.GetByTickerSymbolAsync(tickerSymbol);
            return stock != null ? _mapper.Map<StockDTO>(stock) : null;
        }

        /// <summary>
        /// Asynchronously retrieves stocks by a collection of ticker symbols and maps them to their DTO representation.
        /// </summary>
        /// <param name="tickerSymbols">The collection of ticker symbols.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of <see cref="StockDTO"/>.</returns>
        public async Task<IEnumerable<StockDTO>> GetStocksByTickerSymbolsAsync(IEnumerable<string> tickerSymbols)
        {
            var stocks = await _stockRepository.GetByTickerSymbolsAsync(tickerSymbols);
            return _mapper.Map<IEnumerable<StockDTO>>(stocks);
        }

        /// <summary>
        /// Asynchronously updates the price of a stock identified by its ticker symbol and maps the updated stock to its DTO representation.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock to update.</param>
        /// <param name="newPrice">The new price to set for the stock.</param>
        /// <returns>A task representing the asynchronous operation, containing the updated <see cref="StockDTO"/> or null if the stock was not found.</returns>
        public async Task<StockDTO> UpdateStockPriceAsync(string tickerSymbol, decimal newPrice)
        {
            var stock = await _stockRepository.GetByTickerSymbolAsync(tickerSymbol);
            if (stock != null)
            {
                stock.CurrentPrice = newPrice;
                await _stockRepository.UpdateAsync(stock);
                return _mapper.Map<StockDTO>(stock);
            }
            return null;
        }
    }
}