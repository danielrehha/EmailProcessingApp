using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Domain.Models;

namespace EmailProcessingApp.Application.Contract.Logging
{
    public interface IBlobService
    {
        /// <summary>
        /// Appends content to existing blob file or creates new blob and appends content if the specified blob does not exist.
        /// </summary>
        /// <param name="blobName"></param>
        /// <param name="content"></param>
        /// <param name="containerType"></param>
        /// <returns></returns>
        Task AppendToBlobAsync(string blobName, string content, BlobContainerType containerType);

        /// <summary>
        /// Downloads blob from the specified container.
        /// </summary>
        /// <param name="blobName"></param>
        /// <param name="containerType"></param>
        /// <returns></returns>
        Task<byte[]> DownloadBlobAsync(string blobName, BlobContainerType containerType);
    }
}
