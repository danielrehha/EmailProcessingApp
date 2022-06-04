using EmailProcessingApp.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace EmailProcessingApp.Application.Features
{
    public static class ValidationResultHelper
    {
        public static ValidationResult Resolve<T>(this ValidationResult result, T response) where T : ClientCommandResponse
        {
            if (result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
            return result;
        }
    }
}
