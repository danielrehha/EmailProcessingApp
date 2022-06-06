using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Domain.Models;

namespace EmailProcessingApp.Application.Contract.Persistence
{
    public interface IEmailDataRepository : IAsyncRepository<EmailData>
    {
        Task<bool> ValidateAttributeListAsync(string email, List<string> attributes);
        Task<List<EmailData>> GetRangeByEmailAddressAsync(string email, DateTime from, DateTime until);
    }
}
