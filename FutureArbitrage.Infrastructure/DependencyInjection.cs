using FutureArbitrage.Application.Services.Abstructions;
using FutureArbitrage.Domain.Abstractions;
using FutureArbitrage.Domain.Abstractions.IRepositories;
using FutureArbitrage.Domain.Common;
using FutureArbitrage.Infrastructure.Data.Context;
using FutureArbitrage.Infrastructure.Implimentations;
using FutureArbitrage.Infrastructure.Implimentations.Repositories;
using FutureArbitrage.Infrastructure.Services;
using FutureArbitrage.Infrastructure.Services.Implimentations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FutureArbitrage.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Registr Repositories
            services.AddScoped<IBaseRepository<Entity>, BaseRepository<Entity>>();
            services.AddScoped<IArbitrageResultRepository, ArbitrageResultRepository>();
            services.AddScoped<IFutureContractRepository, FutureContractRepository>();
            services.AddScoped<IFuturePriceRepository, FuturePriceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWOrk>();

            // Registr Services
            services.AddHttpClient();
            services.AddScoped<IExchangePriceServiceFactory, ExchangePriceServiceFactory>();
            services.AddScoped<IPriceServiceContextStrategy, PriceServiceContextStrategy>();
            services.AddScoped<IExchangePriceServiceStrategy, BinancePriceService>();

            //services.AddScoped<IPriceServiceContextStrategy>(serviceProvider =>
            //{
            //    var factory = serviceProvider.GetRequiredService<IExchangePriceServiceFactory>(); 
            //    var defaultExchange = ExchangeTypeEnum.Binance;
            //    var defaultService = factory.CreatePriceService(defaultExchange);
            //    return new PriceServiceContextStrategy(defaultService);
            //});

            string connectionString = configuration.GetConnectionString("ConnectionString")!;
            services.AddDbContext<FutureArbitrageDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging(false);
            }, ServiceLifetime.Scoped);
            return services;
        }
    }
}
