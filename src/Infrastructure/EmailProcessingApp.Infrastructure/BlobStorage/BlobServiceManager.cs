using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Application.Extensions;
using Microsoft.Extensions.Configuration;

namespace EmailProcessingApp.Infrastructure.BlobStorage
{
    public class BlobServiceManager : IBlobServiceManager
    {
        private readonly IConfiguration _configuration;
        
        public BlobServiceManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString()
        {
            return _configuration.GetConnectionString("BlobConnection");
        }

        public string GetContainerName(BlobContainerType containerType)
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
