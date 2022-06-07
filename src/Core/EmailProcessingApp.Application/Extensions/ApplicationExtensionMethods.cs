using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Domain.Models;
using FluentValidation;
using Newtonsoft.Json;

namespace EmailProcessingApp.Application.Extensions
{
    public static class ApplicationExtensionMethods
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

        /// <summary>
        /// Converts incoming client payload to formatted log data, including validation results.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string ToLogData(this BaseDto dto)
        {
            return $"[{DateTime.UtcNow}] Incoming client payload\n" + JsonConvert.SerializeObject(dto, Formatting.Indented) + Environment.NewLine;
        }

        public static string ToBlobName(this EmailDataDto dto)
        {
            return DateTime.Now.ToShortDateString().Replace("/", "-") + "_" + dto.Email + ".txt";
        }

        /// <summary>
        /// Returns appsettings.json container entry key. Throws exception if entry is not present in the file.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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
            }

            throw new NotImplementedException(type.ToString());
        }

        /// <summary>
        /// Returns appsettings.json file entry key. Throws exception if entry is not present in the file.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string ToFileKey(this MessageTemplateType type)
        {
            switch (type)
            {
                case MessageTemplateType.ResponseEmailBody:
                    return "ResponseEmailBodyTemplate";
            }

            throw new NotImplementedException(type.ToString());
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
