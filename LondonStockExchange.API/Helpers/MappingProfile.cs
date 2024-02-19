using AutoMapper;
using LondonStockExchange.API.DTOs;
using LondonStockExchange.API.Models;

namespace LondonStockExchange.API.Helpers
{
    public class MappingProfile : Profile
    {
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