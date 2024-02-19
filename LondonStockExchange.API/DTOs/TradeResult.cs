namespace LondonStockExchange.API.DTOs
{
    public class TradeResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public StockDTO UpdatedStock { get; set; }
    }
}