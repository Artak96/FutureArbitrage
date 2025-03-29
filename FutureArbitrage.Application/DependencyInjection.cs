using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.Extensions.DependencyInjection;
using FutureArbitrage.Application.Services.Abstructions;
using FutureArbitrage.Application.Services.Implimentations;


namespace FutureArbitrage.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IArbitrageCalculatorService, ArbitrageCalculatorService>();

            services.AddMediatR();
            services.AddValidation();
            return services;
        }

        private static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                //Assembly[] handlersAssemblies = new[]
                //{
                //    //typeof(INotificationHandlerEvent).GetTypeInfo().Assembly,
                //};
                //config.RegisterServicesFromAssemblies(handlersAssemblies);
                config.NotificationPublisher = new TaskWhenAllPublisher();
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection serviceCollection)
        {
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
