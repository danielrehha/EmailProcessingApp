using EmailProcessingApp.Application.Enums;

namespace EmailProcessingApp.Application.Contract.Static
{
    public interface IMessageTemplateService
    {
        Task<string> GetMessageTemplateAsync(MessageTemplateType type);
    }
}
