using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Domain.Models;

namespace EmailProcessingApp.Application.Contract.Logging
{
    public interface IBlobService
    {
        Task AppendToBlobAsync(string blobName, string content, BlobContainerType containerType);
        Task<byte[]> DownloadBlobAsync(string blobName, BlobContainerType containerType);
    }
}
