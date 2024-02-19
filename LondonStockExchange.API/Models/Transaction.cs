namespace LondonStockExchange.API.Models
{
    /// <summary>
    /// Represents a transaction in the financial trading platform.
    /// A transaction records the exchange of stock shares between buyers and sellers,
    /// capturing the price, number of shares, and the precise moment the transaction occurred.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        /// <value>The transaction's unique identifier, as an integer.</value>
        public int TransactionID { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the stock involved in the transaction.
        /// This foreign key links the transaction to a specific stock.
        /// </summary>
        /// <value>The identifier of the stock involved in the transaction, as an integer.</value>
        public int StockID { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the broker facilitating the transaction.
        /// This foreign key links the transaction to the broker responsible for it.
        /// </summary>
        /// <value>The identifier of the broker facilitating the transaction, as an integer.</value>
        public int BrokerID { get; set; }

        /// <summary>
        /// Gets or sets the price at which the stock shares were exchanged during the transaction.
        /// </summary>
        /// <value>The price of the stock shares at the time of the transaction, as a decimal.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the number of stock shares exchanged in the transaction.
        /// </summary>
        /// <value>The number of shares exchanged, as a decimal to allow for fractional share transactions.</value>
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets or sets the timestamp marking when the transaction occurred.
        /// </summary>
        /// <value>The timestamp of the transaction, as a <see cref="DateTime"/>.</value>
        public DateTime Timestamp { get; set; }

        // Navigation properties for EF Core - Establishes the relationships to Stock and Broker

        /// <summary>
        /// Navigation property to the stock involved in the transaction.
        /// This property allows direct access to the stock's details from a transaction instance.
        /// </summary>
        /// <value>The <see cref="Stock"/> entity associated with this transaction.</value>
        public Stock Stock { get; set; }

        /// <summary>
        /// Navigation property to the broker facilitating the transaction.
        /// This property allows direct access to the broker's details from a transaction instance.
        /// </summary>
        /// <value>The <see cref="Broker"/> entity responsible for this transaction.</value>
        public Broker Broker { get; set; }
    }
}