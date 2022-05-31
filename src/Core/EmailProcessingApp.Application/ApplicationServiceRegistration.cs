using EmailProcessingApp.Application.Contract;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmailProcessingApp.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
