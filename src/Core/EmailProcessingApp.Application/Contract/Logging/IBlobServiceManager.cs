using EmailProcessingApp.Application.Enums;

namespace EmailProcessingApp.Application.Contract.Logging
{
    public interface IBlobServiceManager
    {
        string ConnectionString();
        string GetContainerName(BlobContainerType containerType);
    }
}
