using System.ComponentModel.DataAnnotations;

namespace LondonStockExchange.API.DTOs
{
    /// <summary>
    /// Represents the data transfer object for initiating a trade on the stock exchange.
    /// It encapsulates the necessary information for executing a trade including the stock,
    /// price, shares, and the broker responsible for the trade.
    /// </summary>
    public class TradeDTO
    {
        /// <summary>
        /// Gets or sets the ticker symbol of the stock being traded.
        /// </summary>
        /// <remarks>
        /// The ticker symbol is a unique identifier for a publicly traded stock, 
        /// and must consist of 1 to 5 uppercase letters, validated via a regular expression.
        /// </remarks>
        [Required(ErrorMessage = "Ticker symbol is required.")]
        [RegularExpression(@"^[A-Z]{1,5}$", ErrorMessage = "Ticker symbol must be up to 5 uppercase letters.")]
        public string TickerSymbol { get; set; }

        /// <summary>
        /// Gets or sets the price per share at which the trade is executed.
        /// </summary>
        /// <remarks>
        /// The price must be a positive value greater than zero, ensuring the trade's validity.
        /// </remarks>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the number of shares involved in the trade.
        /// </summary>
        /// <remarks>
        /// The share count must be a positive decimal greater than zero, allowing for fractional share trading.
        /// </remarks>
        [Range(0.01, double.MaxValue, ErrorMessage = "Shares must be greater than zero.")]
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the broker executing the trade.
        /// </summary>
        /// <remarks>
        /// The broker ID is essential for associating the trade with a specific broker,
        /// ensuring accountability and traceability of the trade execution.
        /// </remarks>
        [Required(ErrorMessage = "Broker ID is required.")]
        public int BrokerId { get; set; }
    }
}