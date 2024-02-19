namespace LondonStockExchange.API.Models
{
    /// <summary>
    /// Represents a stock entity in the financial trading platform.
    /// Stocks are identifiable by their ticker symbols and have an associated current market price.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Gets or sets the unique identifier for the stock.
        /// </summary>
        /// <value>The stock's unique identifier, as an integer.</value>
        public int StockID { get; set; }

        /// <summary>
        /// Gets or sets the ticker symbol of the stock.
        /// The ticker symbol is a unique sequence of letters representing the publicly traded shares of a particular stock on a stock exchange.
        /// </summary>
        /// <value>The ticker symbol, as a string.</value>
        public string TickerSymbol { get; set; }

        /// <summary>
        /// Gets or sets the current market price of the stock.
        /// This price is subject to change based on market conditions and trading activities.
        /// </summary>
        /// <value>The current market price, as a decimal.</value>
        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// Navigation property that provides access to the transactions associated with this stock.
        /// This property facilitates the lazy loading of transactions, meaning the transaction data is only loaded
        /// when it is specifically accessed. This is managed through Entity Framework Core, denoting a one-to-many
        /// relationship between a stock and its transactions.
        /// </summary>
        /// <value>A collection of transactions related to this stock.</value>
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}