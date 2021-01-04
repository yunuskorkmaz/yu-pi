

namespace App.Api.Extensions
{

    public class SpaFallbackMiddleware
    {
        private readonly RequestDelegate _next;
        private SpaFallbackOptions _options;
        public SpaFallbackMiddleware(RequestDelegate next, SpaFallbackOptions options)
        {

        }
    }
}