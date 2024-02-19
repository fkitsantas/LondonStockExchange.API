using System.ComponentModel.DataAnnotations;

namespace LondonStockExchange.API.DTOs
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{1,5}$", ErrorMessage = "Ticker symbol must be 1 to 5 uppercase letters.")]
        public string TickerSymbol { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Shares must be greater than zero.")]
        public decimal Shares { get; set; }

        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Broker ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Broker ID must be a positive number.")]
        public int BrokerId { get; set; }
    }
}