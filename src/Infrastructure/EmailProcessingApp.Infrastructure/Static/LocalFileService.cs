using EmailProcessingApp.Application.Contract.Static;

namespace EmailProcessingApp.Infrastructure.Static
{
    public class LocalFileService : ILocalFileService
    {
        public async Task<byte[]> ReadAsync(string filePath)
        {
            var file = await File.ReadAllBytesAsync(filePath);
            return file;
        }

        public async Task SaveAsync(string filePath, byte[] content)
        {
            if(content.Length < 1)
            {
                throw new OperationCanceledException("File has no content.");
            }

            await File.WriteAllBytesAsync(filePath, content);
        }
    }
}
