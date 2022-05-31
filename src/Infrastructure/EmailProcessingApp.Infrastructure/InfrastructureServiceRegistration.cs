using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace EmailProcessingApp.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ILogServiceManager, LogServiceManager>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
    }
}
