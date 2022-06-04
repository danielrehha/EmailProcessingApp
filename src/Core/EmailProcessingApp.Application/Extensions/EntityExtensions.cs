using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Domain.Models;
using FluentValidation;
using Newtonsoft.Json;

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

        public static string ToLogData(this EmailDataDto dto, BaseResponse response)
        {
            var logData = new
            {
                ClientPayload = dto,
                Response = response,
            };
            return $"[{DateTime.UtcNow}] Incoming client payload\n" + JsonConvert.SerializeObject(logData, Formatting.Indented) + Environment.NewLine;
        }

        public static string ToBlobName(this EmailDataDto dto)
        {
            return DateTime.Now.ToShortDateString().Replace("/", "-") + "_" + dto.Email + ".txt";
        }

        public static string ToContainerConfigurationKey(this BlobContainerType type)
        {
            switch (type)
            {
                case BlobContainerType.EmailLogContainer:
                    return "EmailDataPayloadContainer";
                case BlobContainerType.EmailBodyContainer:
                    return "ResponseEmailBodyContainer";
                case BlobContainerType.MessageTemplateContainer:
                    return "MessageTemplateContainer";
                default:
                    return "Default";
            }
        }

        public static string ToFileName(this MessageTemplateType type)
        {
            switch (type)
            {
                case MessageTemplateType.ResponseEmailBody:
                    return "response-email-template.txt";
                default:
                    return "default-template.txt";
            }
        }

        public static string ToProblemDetails(this ValidationException ex)
        {
            var details = new
            {
                StatusCode = 400,
                Errors = ex.Errors.Select(x => x.ErrorMessage).ToList()
            };

            return JsonConvert.SerializeObject(details, Formatting.Indented);
        }
    }
}
