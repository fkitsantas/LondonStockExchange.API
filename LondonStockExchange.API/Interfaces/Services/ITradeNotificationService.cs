namespace LondonStockExchange.API.Interfaces.Services
{
    public interface ITradeNotificationService
    {
        Task NotifyAllClientsAsync(string tickerSymbol, decimal price, decimal shares, int brokerId);
    }
}
