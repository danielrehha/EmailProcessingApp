using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Enums;
using System.Text;

namespace EmailProcessingApp.Infrastructure.Logging
{
    public class BlobService : IBlobService
    {
        private readonly IBlobServiceManager _logServiceManager;

        public BlobService(IBlobServiceManager logServiceManager)
        {
            _logServiceManager = logServiceManager;
        }

        public async Task AppendToBlobAsync(string blobName, string content, BlobContainerType containerType)
        {
            var client = new BlobServiceClient(_logServiceManager.ConnectionString());

            var targetContainerName = _logServiceManager.GetContainerName(containerType);

            var container = client.GetBlobContainerClient(targetContainerName);

            await container.CreateIfNotExistsAsync();

            var blobClient = new AppendBlobClient(_logServiceManager.ConnectionString(), targetContainerName, blobName);

            await blobClient.CreateIfNotExistsAsync();

            var bytes = Encoding.UTF8.GetBytes(content);

            var stream = new MemoryStream(bytes);

            await blobClient.AppendBlockAsync(stream);
        }

        public async Task<byte[]> DownloadBlobAsync(string blobName, BlobContainerType containerType)
        {
            var client = new BlobServiceClient(_logServiceManager.ConnectionString());

            var targetContainerName = _logServiceManager.GetContainerName(containerType);

            var container = client.GetBlobContainerClient(targetContainerName);

            await container.CreateIfNotExistsAsync();

            var blobClient = new BlobClient(_logServiceManager.ConnectionString(), targetContainerName, blobName);

            var resultStream = await blobClient.DownloadStreamingAsync();

            using (var memoryStream = new MemoryStream())
            {
                await resultStream.Value.Content.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<byte[]> DownloadBlobAsync(string blobName, BlobContainerType containerType, string localFilePath)
        {
            var client = new BlobServiceClient(_logServiceManager.ConnectionString());

            var targetContainerName = _logServiceManager.GetContainerName(containerType);

            var container = client.GetBlobContainerClient(targetContainerName);

            await container.CreateIfNotExistsAsync();

            var blobClient = new BlobClient(_logServiceManager.ConnectionString(), targetContainerName, blobName);

            if(!await blobClient.ExistsAsync())
            {
                var fileBytes = await File.ReadAllBytesAsync(localFilePath);
                var stream = new MemoryStream(fileBytes);
                await blobClient.UploadAsync(stream);
                return fileBytes;
            }

            var resultStream = await blobClient.DownloadStreamingAsync();

            if (File.Exists(localFilePath))
            {
                var remoteFileProperties = await blobClient.GetPropertiesAsync();
                var localFileBytes = await File.ReadAllBytesAsync(localFilePath);
                if (remoteFileProperties.Value.ContentLength != localFileBytes.Length)
                {
                    var memoryStream = new MemoryStream();
                    resultStream.Value.Content.CopyTo(memoryStream);
                    await File.WriteAllBytesAsync(localFilePath, memoryStream.ToArray());
                }
            }

            using (var memoryStream = new MemoryStream())
            {
                await resultStream.Value.Content.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
