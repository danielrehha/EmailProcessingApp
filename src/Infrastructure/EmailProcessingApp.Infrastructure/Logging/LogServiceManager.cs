using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Application.Extensions;
using Microsoft.Extensions.Configuration;

namespace EmailProcessingApp.Infrastructure.Logging
{
    public class LogServiceManager : ILogServiceManager
    {
        private readonly IConfiguration _configuration;
        
        public LogServiceManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString()
        {
            return _configuration.GetConnectionString("Blob");
        }

        public string LogContainer(BlobContainerType containerType)
        {
            var containerConfigurationKey = containerType.ToContainerConfigurationKey();

            return _configuration
                .GetSection("Configuration")
                .GetSection("Blob")
                .GetSection("Containers")
                .GetSection(containerConfigurationKey)
                .Value;
        }
    }
}
