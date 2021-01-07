using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddAppDependencyInjection(this IServiceCollection services)
        {

            return services;
        }
    }
}