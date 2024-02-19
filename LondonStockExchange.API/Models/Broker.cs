using System.Collections.Generic;

namespace LondonStockExchange.API.Models
{
    /// <summary>
    /// Represents a broker entity within the trading platform.
    /// Brokers are responsible for managing trade transactions on behalf of their clients.
    /// </summary>
    public class Broker
    {
        /// <summary>
        /// Gets or sets the unique identifier for the broker.
        /// </summary>
        /// <value>The broker's unique identifier.</value>
        public int BrokerID { get; set; }

        /// <summary>
        /// Gets or sets the name of the broker.
        /// The name is used to identify the broker within the platform.
        /// </summary>
        /// <value>The name of the broker.</value>
        public string Name { get; set; }

        /// <summary>
        /// Navigation property for accessing the transactions associated with this broker.
        /// This property enables lazy loading of transactions, meaning that transaction data is only loaded
        /// from the database when it is accessed. This relationship is established via Entity Framework Core
        /// and represents a one-to-many relationship between a broker and transactions.
        /// </summary>
        /// <value>A collection of transactions associated with the broker.</value>
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}