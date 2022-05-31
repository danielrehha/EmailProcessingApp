using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Domain.Models;

namespace EmailProcessingApp.Application.Extensions
{
    public static class EntityExtensions
    {
        public static EmailData ToEmailData(this EmailDataDto dto)
        {
            var emailData = new EmailData();

            emailData.Key = dto.Key;
            emailData.Email = dto.Email;
            emailData.Attributes = string.Join(",", dto.Attributes);

            return emailData;
        }

        public static EmailDataDto ToDto(this EmailData emailData)
        {
            var dto = new EmailDataDto();

            dto.Key = emailData.Key;
            dto.Email = emailData.Email;
            dto.Attributes = emailData.Attributes.Split(",").ToList();

            return dto;
        }

        public static string ToContainerConfigurationKey(this BlobContainerType type)
        {
            if (type == BlobContainerType.EmailLogContainer)
            {
                return "EmailDataPayloadLogging";
            }
            return "Default";
        }
    }
}
