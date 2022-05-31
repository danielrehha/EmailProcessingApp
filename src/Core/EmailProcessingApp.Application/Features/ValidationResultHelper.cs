using EmailProcessingApp.Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailProcessingApp.Application.Features
{
    public static class ValidationResultHelper
    {
        public static ValidationResult Resolve<T>(this ValidationResult result, T response) where T : BaseResponse
        {
            if (result.Errors.Count > 0)
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessage = result.Errors.First().ErrorMessage;
            }
            return result;
        }
    }
}
