using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Domain.Models;

namespace EmailProcessingApp.Application.Contract.Persistence
{
    public interface IEmailDataRepository : IAsyncRepository<EmailData>
    {
        Task<bool> IsAttributeListUnique(EmailDataDto dto);
    }
}
