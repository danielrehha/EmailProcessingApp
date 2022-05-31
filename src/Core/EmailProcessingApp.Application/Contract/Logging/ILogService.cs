using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Domain.Models;

namespace EmailProcessingApp.Application.Contract.Logging
{
    public interface ILogService
    {
        Task LogEmailDataPayloadAsync(EmailDataDto clientPayloadDto, BaseResponse response);
    }
}
