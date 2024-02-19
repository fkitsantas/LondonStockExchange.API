using Microsoft.Extensions.DependencyInjection;
using LondonStockExchange.API.Repositories;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Services;
using LondonStockExchange.API.Interfaces.Services;
using System.Reflection;

namespace LondonStockExchange.API.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IBrokerRepository, BrokerRepository>();

            // Services
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<IStockService, StockService>();

            // AutoMapper registration
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}