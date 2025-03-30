using FluentValidation;
using FluentValidation.AspNetCore;
using FutureArbitrage.Application.Services.Abstructions;
using FutureArbitrage.Application.Services.Implimentations;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;


namespace FutureArbitrage.Application
{
    public static class DependencyInjection
    {
        private static ILogger _logger = Log.ForContext(typeof(DependencyInjection));

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IArbitrageCalculatorService, ArbitrageCalculatorService>();

            services.AddMediatR();
            services.AddValidation();
            return services;
        }

        private static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            _logger.Information("Mediator service starting....");

            services.AddMediatR(config =>
            {
                config.NotificationPublisher = new TaskWhenAllPublisher();
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection serviceCollection)
        {
            _logger.Information("Validation service starting....");

            serviceCollection.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            serviceCollection.AddValidatorsFromAssemblyContaining<AssemblyReference>();
            serviceCollection.AddFluentValidationAutoValidation(x => x.DisableDataAnnotationsValidation = true);
            serviceCollection.AddFluentValidationClientsideAdapters();

            return serviceCollection;
        }
    }
}
