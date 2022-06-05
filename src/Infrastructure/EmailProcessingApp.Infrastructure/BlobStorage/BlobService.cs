using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Enums;
using System.Text;

namespace EmailProcessingApp.Infrastructure.BlobStorage
{
    public class BlobService : IBlobService
    {
        private readonly IBlobServiceManager _blobServiceManager;

        public BlobService(IBlobServiceManager logServiceManager)
        {
            _blobServiceManager = logServiceManager;
        }

        public async Task AppendToBlobAsync(string blobName, string content, BlobContainerType containerType)
        {
            var client = new BlobServiceClient(_blobServiceManager.ConnectionString());

            var targetContainerName = _blobServiceManager.GetContainerName(containerType);

            var container = client.GetBlobContainerClient(targetContainerName);

            await container.CreateIfNotExistsAsync();

            var blobClient = new AppendBlobClient(_blobServiceManager.ConnectionString(), targetContainerName, blobName);

            await blobClient.CreateIfNotExistsAsync();

            var bytes = Encoding.UTF8.GetBytes(content);

            var stream = new MemoryStream(bytes);

            await blobClient.AppendBlockAsync(stream);
        }

        public async Task<byte[]> DownloadBlobAsync(string blobName, BlobContainerType containerType)
        {
            var client = new BlobServiceClient(_blobServiceManager.ConnectionString());

            var targetContainerName = _blobServiceManager.GetContainerName(containerType);

            var container = client.GetBlobContainerClient(targetContainerName);

            await container.CreateIfNotExistsAsync();

            var blobClient = new BlobClient(_blobServiceManager.ConnectionString(), targetContainerName, blobName);

            if(!await blobClient.ExistsAsync())
            {
                return null;
            }

            var resultStream = await blobClient.DownloadStreamingAsync();

            using (var memoryStream = new MemoryStream())
            {
                await resultStream.Value.Content.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
