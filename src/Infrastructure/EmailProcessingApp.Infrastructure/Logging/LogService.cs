using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Domain.Models;
using Newtonsoft.Json;
using System.Text;

namespace EmailProcessingApp.Infrastructure.Logging
{
    public class LogService : ILogService
    {
        private readonly ILogServiceManager _logServiceManager;

        public LogService(ILogServiceManager logServiceManager)
        {
            _logServiceManager = logServiceManager;
        }

        public async Task LogEmailDataPayloadAsync(EmailDataDto clientPayloadDto, BaseResponse response)
        {
            var client = new BlobServiceClient(_logServiceManager.ConnectionString());

            var targetContainerName = _logServiceManager.LogContainer(BlobContainerType.EmailLogContainer);

            var container = client.GetBlobContainerClient(targetContainerName);

            await container.CreateIfNotExistsAsync();

            var blobName = DateTime.Now.ToShortDateString() + "_" + clientPayloadDto.Email + ".txt";
            blobName = blobName.Replace("/", "-");

            var blobClient = new AppendBlobClient(_logServiceManager.ConnectionString(), targetContainerName, blobName);

            await blobClient.CreateIfNotExistsAsync();

            var logData = new
            {
                ClientPayload = clientPayloadDto,
                Response = response,
            };

            var logText = $"[{DateTime.UtcNow}] Incoming client payload\n" + JsonConvert.SerializeObject(logData, Formatting.Indented) + Environment.NewLine;

            var bytes = Encoding.UTF8.GetBytes(logText);

            var stream = new MemoryStream(bytes);

            await blobClient.AppendBlockAsync(stream);
        }
    }
}
