using LondonStockExchange.API.Hubs;
using LondonStockExchange.API.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Services
{
    /// <summary>
    /// Implements the <see cref="ITradeNotificationService"/> to notify all connected clients about trade operations.
    /// </summary>
    public class TradeNotificationService : ITradeNotificationService
    {
        private readonly IHubContext<TradeNotificationHub> _hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeNotificationService"/> class.
        /// </summary>
        /// <param name="hubContext">The hub context for SignalR, allowing communication with clients.</param>
        public TradeNotificationService(IHubContext<TradeNotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// Notifies all connected clients about a trade operation.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock traded.</param>
        /// <param name="price">The price at which the stock was traded.</param>
        /// <param name="shares">The number of shares traded.</param>
        /// <param name="brokerId">The identifier of the broker who managed the trade.</param>
        /// <returns>A task that represents the asynchronous operation of sending the trade notification.</returns>
        /// <remarks>
        /// This method leverages SignalR to broadcast the trade information to all connected clients in real-time,
        /// ensuring that subscribers are immediately informed of new trades.
        /// </remarks>
        public async Task NotifyAllClientsAsync(string tickerSymbol, decimal price, decimal shares, int brokerId)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveTradeInfo", tickerSymbol, price, shares, brokerId);
        }
    }
}