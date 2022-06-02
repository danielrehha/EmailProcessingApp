using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Domain.Models;

namespace EmailProcessingApp.Application.Contract.Persistence
{
    public interface IEmailDataRepository : IAsyncRepository<EmailData>
    {
        Task<bool> IsAttributeListUniqueAsync(EmailDataDto dto);
        Task<List<EmailData>> GetRangeByEmailAddressAsync(string email, DateTime from, DateTime until);
    }
}
