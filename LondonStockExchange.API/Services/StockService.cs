using AutoMapper;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockDTO>> GetAllStocksAsync()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StockDTO>>(stocks);
        }

        public async Task<StockDTO> GetStockByTickerSymbolAsync(string tickerSymbol)
        {
            var stock = await _stockRepository.GetByTickerSymbolAsync(tickerSymbol);
            return _mapper.Map<StockDTO>(stock);
        }

        public async Task<IEnumerable<StockDTO>> GetStocksByTickerSymbolsAsync(IEnumerable<string> tickerSymbols)
        {
            var stocks = await _stockRepository.GetByTickerSymbolsAsync(tickerSymbols);
            return _mapper.Map<IEnumerable<StockDTO>>(stocks);
        }

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