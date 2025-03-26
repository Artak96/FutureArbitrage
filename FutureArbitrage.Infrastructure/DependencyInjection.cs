using FutureArbitrage.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FutureArbitrage.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IBaseRepository, BaseRepository>();
            //services.AddScoped<IUnitOfWork, UnitOfWOrk>();

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
