namespace EmailProcessingApp.Application.Contract.Persistence
{
    /// <summary>
    /// Contract for base repository operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> QueryAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(Guid id, T entity);
        Task<T> DeleteAsync(Guid id);
        Task<bool> DoesExistAsync(Guid id);
    }
}
