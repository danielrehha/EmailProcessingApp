using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Contract.Static;
using EmailProcessingApp.Infrastructure.Logging;
using EmailProcessingApp.Infrastructure.Static;
using Microsoft.Extensions.DependencyInjection;

namespace EmailProcessingApp.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IBlobServiceManager, BlobServiceManager>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IMessageTemplateService, MessageTemplateService>();

            return services;
        }
    }
}
