using System.ComponentModel.DataAnnotations;

namespace LondonStockExchange.API.DTOs
{
    public class TradeDTO
    {
        [Required]
        [RegularExpression(@"^[A-Z]{1,5}$", ErrorMessage = "Ticker symbol must be up to 5 uppercase letters.")]
        public string TickerSymbol { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Shares must be greater than zero.")]
        public decimal Shares { get; set; }

        [Required(ErrorMessage = "Broker ID is required.")]
        public int BrokerId { get; set; }
    }
}