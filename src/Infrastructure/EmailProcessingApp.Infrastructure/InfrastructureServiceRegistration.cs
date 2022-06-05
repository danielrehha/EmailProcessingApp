using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Contract.Static;
using EmailProcessingApp.Infrastructure.BlobStorage;
using EmailProcessingApp.Infrastructure.Static;
using Microsoft.Extensions.DependencyInjection;

namespace EmailProcessingApp.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IBlobServiceManager, BlobServiceManager>();
            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton<IMessageTemplateService, MessageTemplateService>();

            return services;
        }
    }
}
