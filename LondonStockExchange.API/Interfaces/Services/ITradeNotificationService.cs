using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Services
{
    /// <summary>
    /// Defines a service for sending trade notifications to all connected clients.
    /// </summary>
    public interface ITradeNotificationService
    {
        /// <summary>
        /// Notifies all clients about a trade operation.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock traded.</param>
        /// <param name="price">The price at which the stock was traded.</param>
        /// <param name="shares">The number of shares traded.</param>
        /// <param name="brokerId">The identifier of the broker who managed the trade.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method utilizes SignalR to broadcast real-time trade notifications to all
        /// subscribers, providing instant updates on trade activities across the platform.
        /// </remarks>
        Task NotifyAllClientsAsync(string tickerSymbol, decimal price, decimal shares, int brokerId);
    }
}