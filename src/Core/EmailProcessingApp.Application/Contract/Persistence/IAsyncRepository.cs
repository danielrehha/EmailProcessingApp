namespace EmailProcessingApp.Application.Contract.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> QueryAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(object id, T entity);
        Task<T> DeleteAsync(object id);
        Task<bool> DoesExistAsync(object id);
    }
}
