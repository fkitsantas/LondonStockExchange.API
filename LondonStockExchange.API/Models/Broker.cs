namespace LondonStockExchange.API.Models
{
    public class Broker
    {
        public int BrokerID { get; set; }
        public string Name { get; set; }

        // Navigation Property for EF Core - Represents the Transactions this Broker is associated with
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}