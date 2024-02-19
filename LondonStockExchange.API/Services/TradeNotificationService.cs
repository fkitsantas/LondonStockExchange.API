using LondonStockExchange.API.Hubs;
using LondonStockExchange.API.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace LondonStockExchange.API.Services
{
    public class TradeNotificationService : ITradeNotificationService
    {
        private readonly IHubContext<TradeNotificationHub> _hubContext;

        public TradeNotificationService(IHubContext<TradeNotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyAllClientsAsync(string tickerSymbol, decimal price, decimal shares, int brokerId)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveTradeInfo", tickerSymbol, price, shares, brokerId);
        }
    }
}
