using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddAppDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<INgrokService,NgrokService>();
            services.AddSingleton<AblyClientService,AblyClientService>();
            return services;
        }
    }
}