using Api.Helpers;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class ContextExtension
    {
        public static IServiceCollection AddAppContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = DatabaseHelper.GetConnectionString(configuration);

            return services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        }
    }
}