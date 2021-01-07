using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Yupi.Web.Core.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        // public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        // {
        //     return builder.UseMiddleware<ExceptionMiddleware>();
        // }

        // public static IMvcBuilder AddControllersWithValidationFilter(this IServiceCollection services)
        // {
        //     services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
		// 	{
		// 		apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
		// 	});
        //     return services.AddControllers( option => {
        //         option.Filters.Add<ValidationFilter>();
        //         option.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiExceptionResponse),400));
        //         option.Filters.Add(new ProducesResponseTypeAttribute(typeof(ValidationErrorException),422));
        //     });
            
        // }
    }
}