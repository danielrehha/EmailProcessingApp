using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Contract.Static;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Application.Extensions;
using System.Text;

namespace EmailProcessingApp.Infrastructure.Static
{
    public class MessageTemplateService : IMessageTemplateService
    {
        private readonly IBlobService _blobService;

        public MessageTemplateService(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<string> GetMessageTemplateAsync(MessageTemplateType type)
        {
            var fileName = type.ToFileName();

            if(File.Exists(fileName))
            {
                var bytes = await File.ReadAllBytesAsync(fileName);
                var template = Encoding.UTF8.GetString(bytes);
                return template;
            }

            var fileBytes = await _blobService.DownloadBlobAsync(fileName, BlobContainerType.MessageTemplateContainer);

            return Encoding.UTF8.GetString(fileBytes);
        }
    }
}
