using System.ComponentModel.DataAnnotations;

namespace LondonStockExchange.API.DTOs
{
    /// <summary>
    /// Represents the data transfer object for a transaction on the stock exchange.
    /// This class encapsulates all necessary details about a stock transaction,
    /// including the stock identifier, transaction price, number of shares, and the broker involved.
    /// </summary>
    public class TransactionDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the ticker symbol of the stock involved in the transaction.
        /// Must be 1 to 5 uppercase letters.
        /// </summary>
        [Required(ErrorMessage = "Ticker symbol is required.")]
        [RegularExpression(@"^[A-Z]{1,5}$", ErrorMessage = "Ticker symbol must be 1 to 5 uppercase letters.")]
        public string TickerSymbol { get; set; }

        /// <summary>
        /// Gets or sets the price per share for this transaction. Must be greater than zero.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the number of shares involved in the transaction. Must be greater than zero.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Shares must be greater than zero.")]
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the transaction was executed.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the broker who executed the transaction.
        /// Must be a positive number.
        /// </summary>
        [Required(ErrorMessage = "Broker ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Broker ID must be a positive number.")]
        public int BrokerId { get; set; }
    }
}