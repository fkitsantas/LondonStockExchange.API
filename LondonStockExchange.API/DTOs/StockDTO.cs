using System.ComponentModel.DataAnnotations;

namespace LondonStockExchange.API.DTOs
{ 
    public class StockDTO
    {
        [Required]
        [RegularExpression(@"^[A-Z]{1,5}$", ErrorMessage = "Ticker symbol must be 1 to 5 uppercase letters.")]
        public string TickerSymbol { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Current price must be greater than zero.")]
        public decimal CurrentPrice { get; set; }
    }


}