namespace LondonStockExchange.API.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int StockID { get; set; }
        public int BrokerID { get; set; }
        public decimal Price { get; set; }
        public decimal Shares { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation properties for EF Core - Establishes the relationships to Stock and Broker
        public Stock Stock { get; set; }
        public Broker Broker { get; set; }
    }
}