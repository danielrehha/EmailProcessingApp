namespace EmailProcessingApp.Application.Contract.Static
{
    public interface ILocalFileService
    {
        Task SaveAsync(string filePath, byte[] content);
        Task<byte[]> ReadAsync(string filePath);
    }
}
