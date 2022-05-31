using EmailProcessingApp.Application.Enums;

namespace EmailProcessingApp.Application.Contract.Logging
{
    public interface ILogServiceManager
    {
        string ConnectionString();
        string LogContainer(BlobContainerType containerType);
    }
}
