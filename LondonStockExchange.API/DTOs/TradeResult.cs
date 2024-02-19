namespace LondonStockExchange.API.DTOs
{
    /// <summary>
    /// Represents the outcome of a trade operation, encapsulating success status, 
    /// an informational message, and the updated stock data post-trade.
    /// </summary>
    /// <remarks>
    /// This DTO is designed to provide feedback on the result of a trade action,
    /// enabling clients to programmatically determine the operation's outcome and 
    /// take appropriate actions based on the success status and the potentially updated stock information.
    /// </remarks>
    public class TradeResultDTO
    {
        /// <summary>
        /// Gets or sets a value indicating whether the trade was successfully processed.
        /// </summary>
        /// <value>
        /// True if the trade was processed successfully; otherwise, false.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a message providing details about the trade operation's outcome.
        /// This could include success notifications, error messages, or validation failures.
        /// </summary>
        /// <value>
        /// A string containing the outcome message of the trade operation.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the updated stock data resulting from the trade operation, if applicable.
        /// This property is populated with the current state of the stock after a successful trade.
        /// </summary>
        /// <value>
        /// An instance of <see cref="StockDTO"/> representing the updated stock data; null if the trade was not successful or no update was necessary.
        /// </value>
        public StockDTO UpdatedStock { get; set; }
    }
}