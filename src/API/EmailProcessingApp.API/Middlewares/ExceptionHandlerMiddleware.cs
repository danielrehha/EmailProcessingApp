using EmailProcessingApp.Application.Extensions;
using FluentValidation;

namespace EmailProcessingApp.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.ToProblemDetails());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
