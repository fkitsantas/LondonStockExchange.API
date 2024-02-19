using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Hubs
{
    public class TradeNotificationHub : Hub
    {
        // Method to broadcast trade information to all connected clients
        public async Task BroadcastTradeInfo(string tickerSymbol, decimal price, decimal shares, int brokerId)
        {
            await Clients.All.SendAsync("ReceiveTradeInfo", tickerSymbol, price, shares, brokerId);
        }
    }
}
