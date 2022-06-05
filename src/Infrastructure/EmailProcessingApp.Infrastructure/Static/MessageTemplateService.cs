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

            var fileName = _configuration
                .GetSection("Configuration")
                .GetSection("Blob")
                .GetSection("Templates")
                .GetSection(fileNameKey)
                .Value;

            if (File.Exists(fileName))
            {
                return await File.ReadAllTextAsync(fileName);
            }

            var remoteFileBytes = await _blobService.DownloadBlobAsync(fileName, BlobContainerType.MessageTemplateContainer);

            if (remoteFileBytes == null)
            {
                throw new OperationCanceledException($"Could not find message template '{Enum.GetName(typeof(MessageTemplateType), type)}'.");
            }

            await File.WriteAllBytesAsync(fileName, remoteFileBytes);

            var remoteFileContent = Encoding.UTF8.GetString(remoteFileBytes ?? Array.Empty<byte>());

            return remoteFileContent;
        }
    }
}
