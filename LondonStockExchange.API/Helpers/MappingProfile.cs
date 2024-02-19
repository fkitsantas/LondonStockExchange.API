using AutoMapper;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Models;

namespace LondonStockExchange.API.Helpers
{
    /// <summary>
    /// Configures mapping profiles for AutoMapper to convert between entity models and data transfer objects (DTOs).
    /// This class defines the mappings required to seamlessly convert data to and from models and DTOs,
    /// facilitating data encapsulation and abstraction for API responses or requests.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// Here, all entity-to-DTO and DTO-to-entity mappings are configured.
        /// </summary>
        public MappingProfile()
        {
            // Model to DTO mappings
            CreateMap<Stock, StockDTO>()
                .ForMember(dest => dest.TickerSymbol, opt => opt.MapFrom(src => src.TickerSymbol))
                .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.CurrentPrice));

            CreateMap<Transaction, TransactionDTO>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionID))
                .ForMember(dest => dest.TickerSymbol, opt => opt.MapFrom(src => src.Stock.TickerSymbol))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Shares, opt => opt.MapFrom(src => src.Shares))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.BrokerId, opt => opt.MapFrom(src => src.BrokerID));

            // DTO to Model mappings
            CreateMap<TradeDTO, Transaction>()
                .ForMember(dest => dest.StockID, opt => opt.Ignore())
                .ForMember(dest => dest.BrokerID, opt => opt.MapFrom(src => src.BrokerId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Shares, opt => opt.MapFrom(src => src.Shares))
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore());
        }
    }
}