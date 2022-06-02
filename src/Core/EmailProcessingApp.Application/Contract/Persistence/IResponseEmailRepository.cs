using EmailProcessingApp.Domain.Models;

namespace EmailProcessingApp.Application.Contract.Persistence
{
    public interface IResponseEmailRepository : IAsyncRepository<ResponseEmail>
    {
        Task<ResponseEmail> FindByEmailAndDateAsync(string email, DateTime date);
    }
}
