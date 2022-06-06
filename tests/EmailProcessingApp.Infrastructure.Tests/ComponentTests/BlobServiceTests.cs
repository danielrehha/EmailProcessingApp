using Azure.Storage.Blobs;
using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Infrastructure.BlobStorage;
using EmailProcessingApp.Tests.Shared.Core;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace EmailProcessingApp.Infrastructure.Tests.ComponentTests
{
    [Collection("Serial")]

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class BlobServiceTests : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly BlobService _blobService;

        public BlobServiceTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            var blobServiceInstance = _factory.Services.GetService(typeof(IBlobServiceManager));

            _blobService = new BlobService((IBlobServiceManager)blobServiceInstance);
        }

        [Fact]
        [Priority(0)]
        public async Task Should_Create_Blob()
        {
            var blobName = Guid.NewGuid().ToString() + ".txt";
            var blobContent = Guid.NewGuid().ToString();

            await _blobService.AppendToBlobAsync(blobName, blobContent, BlobContainerType.EmailLogContainer);

            var blobClient = new BlobClient(
                _factory.Configuration
                .GetSection("ConnectionStrings")
                .GetSection("BlobConnection")
                .Value,
                "email-log-container-tests",
                blobName
                );
            var resultStream = await blobClient.DownloadStreamingAsync();
            using var memoryStream = new MemoryStream();
            await resultStream.Value.Content.CopyToAsync(memoryStream);
            var actualContent = Encoding.UTF8.GetString(memoryStream.ToArray());

            await blobClient.DeleteIfExistsAsync();

            Assert.Equal(blobContent, actualContent);
        }

        [Fact]
        [Priority(1)]
        public async Task Should_Download_Blob()
        {
            var blobName = Guid.NewGuid().ToString() + ".txt";
            var blobContent = Guid.NewGuid().ToString();

            var blobClient = new BlobClient(
                _factory.Configuration
                .GetSection("ConnectionStrings")
                .GetSection("BlobConnection")
                .Value,
                "email-log-container-tests",
                blobName
                );
            await blobClient.UploadAsync(new MemoryStream(Encoding.UTF8.GetBytes(blobContent)));

            var resultBytes = await _blobService.DownloadBlobAsync(blobName, BlobContainerType.EmailLogContainer);
            var actualContent = Encoding.UTF8.GetString(resultBytes);

            await blobClient.DeleteIfExistsAsync();

            Assert.Equal(blobContent, actualContent);
        }

        public void Dispose()
        {
           
        }
    }
}
