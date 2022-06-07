using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Contract.Static;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Application.Extensions;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EmailProcessingApp.Infrastructure.Static
{
    public class MessageTemplateService : IMessageTemplateService
    {
        private readonly IBlobService _blobService;
        private readonly IConfiguration _configuration;

        public MessageTemplateService(IBlobService blobService, IConfiguration configuration)
        {
            _blobService = blobService;
            _configuration = configuration;
        }

        public async Task<string> GetMessageTemplateAsync(MessageTemplateType type)
        {
            var fileNameKey = type.ToFileKey();

            // Getting the message template file name from configuration
            var fileName = _configuration
                .GetSection("Configuration")
                .GetSection("Blob")
                .GetSection("Templates")
                .GetSection(fileNameKey)
                .Value;

            // If the file already exists locally we don't fetch it from blob storage again but return it
            if (File.Exists(fileName))
            {
                return await File.ReadAllTextAsync(fileName);
            }

            // Download the template file from blob storage
            var remoteFileBytes = await _blobService.DownloadBlobAsync(fileName, BlobContainerType.MessageTemplateContainer);

            // If there is neither a file locally or in the remote storage we throw an exception
            if (remoteFileBytes == null)
            {
                throw new OperationCanceledException($"Could not find message template '{Enum.GetName(typeof(MessageTemplateType), type)}'.");
            }

            // Save the file to local storage for future usage
            await File.WriteAllBytesAsync(fileName, remoteFileBytes);

            var remoteFileContent = Encoding.UTF8.GetString(remoteFileBytes);

            return remoteFileContent;
        }
    }
}
