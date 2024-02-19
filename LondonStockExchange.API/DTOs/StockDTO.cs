using System.ComponentModel.DataAnnotations;

namespace LondonStockExchange.API.DTOs
{
    /// <summary>
    /// Represents a data transfer object for stock information.
    /// This class is used to transfer stock data between layers without exposing the domain model.
    /// </summary>
    public class StockDTO
    {
        /// <summary>
        /// Gets or sets the ticker symbol for the stock.
        /// The ticker symbol is a unique identifier for publicly traded shares of a particular stock.
        /// </summary>
        /// <remarks>
        /// The ticker symbol must consist of 1 to 5 uppercase letters.
        /// This is enforced through a regular expression validation attribute.
        /// </remarks>
        [Required(ErrorMessage = "Ticker symbol is required.")]
        [RegularExpression(@"^[A-Z]{1,5}$", ErrorMessage = "Ticker symbol must be 1 to 5 uppercase letters.")]
        public string TickerSymbol { get; set; }

        /// <summary>
        /// Gets or sets the current price of the stock.
        /// This represents the latest trading price or the closing price from the last trading session.
        /// </summary>
        /// <remarks>
        /// The current price must be a positive value greater than zero.
        /// This is enforced through a range validation attribute.
        /// </remarks>
        [Range(0.01, double.MaxValue, ErrorMessage = "Current price must be greater than zero.")]
        public decimal CurrentPrice { get; set; }
    }
}