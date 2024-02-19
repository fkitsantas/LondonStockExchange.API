using Microsoft.Extensions.DependencyInjection;
using LondonStockExchange.API.Repositories;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Services;
using LondonStockExchange.API.Interfaces.Services;
using System.Reflection;

namespace LondonStockExchange.API.Configuration
{
    /// <summary>
    /// Provides extension method for <see cref="IServiceCollection"/> to register application services and repositories.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the DI container with scoped lifetime services for repositories and business logic services.
        /// It leverages the <see cref="IServiceCollection"/> to enhance maintainability and decouple the application's
        /// components, following the Dependency Inversion Principle.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> with added services, enabling fluent configuration chaining.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register repository interfaces to their corresponding implementations.
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IBrokerRepository, BrokerRepository>();

            // Register service interfaces to their corresponding implementations.
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<IStockService, StockService>();

            // Automatically registers all AutoMapper profiles in the current assembly.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}