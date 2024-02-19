namespace LondonStockExchange.API.Models
{
    public class Stock
    {
        public int StockID { get; set; }
        public string TickerSymbol { get; set; }
        public decimal CurrentPrice { get; set; }

        // Navigation Property for EF Core - Represents the Transactions associated with this Stock
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}