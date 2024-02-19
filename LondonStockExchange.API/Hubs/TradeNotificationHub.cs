using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Hubs
{
    /// <summary>
    /// A SignalR hub for broadcasting trade information to connected clients in real-time.
    /// This hub enables live updates of trade transactions across the London Stock Exchange platform,
    /// ensuring that clients receive immediate notifications of trade activities.
    /// </summary>
    public class TradeNotificationHub : Hub
    {
        /// <summary>
        /// Broadcasts trade information to all connected clients.
        /// This method allows for real-time updates on trade transactions, including the stock's ticker symbol,
        /// the executed price, the number of shares traded, and the broker ID responsible for the trade.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol of the stock being traded.</param>
        /// <param name="price">The price at which the stock was traded.</param>
        /// <param name="shares">The number of shares involved in the trade.</param>
        /// <param name="brokerId">The identifier of the broker who executed the trade.</param>
        /// <returns>A task that represents the asynchronous operation of broadcasting trade information.</returns>
        public async Task BroadcastTradeInfo(string tickerSymbol, decimal price, decimal shares, int brokerId)
        {
            // The "ReceiveTradeInfo" method will be invoked on all connected clients,
            // passing the trade details as parameters.
            await Clients.All.SendAsync("ReceiveTradeInfo", tickerSymbol, price, shares, brokerId);
        }
    }
}