using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace yu_pi.Infrastructure.Errors
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            string result = null;
            switch (ex)
            {
                case RestException re:
                    context.Response.StatusCode = (int)re.Code;
                    result = JsonSerializer.Serialize(new
                    {
                        errors = re.Errors
                    });
                    break;
                case Exception e:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    logger.LogError(e, "Unhandled Exception");
                    result = JsonSerializer.Serialize(new
                    {
                        errors = "Internal Server Error"
                    });
                    break;
            }
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result ?? "{}");
        }
    }
}