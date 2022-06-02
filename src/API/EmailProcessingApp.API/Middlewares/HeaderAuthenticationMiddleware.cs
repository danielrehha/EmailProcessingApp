namespace EmailProcessingApp.API.Middlewares
{
    public class HeaderAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderKeyName = "HeaderAuthKey";

        public HeaderAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(HeaderKeyName, out
                    var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authentication key was not provided!");
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetSection("Secrets").GetValue<string>(HeaderKeyName);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client!");
                return;
            }
            await _next(context);
        }
    }
}
