using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using yu_pi.Infrastructure.Validation;
using FluentValidation.AspNetCore;
using AutoMapper;
using yu_pi.Infrastructure.Errors;
using yu_pi.Services;

namespace yu_pi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<AblyClientService, AblyClientService>();
            services.AddHostedService<YupiBackgroundService>();

            services.AddYupiContext(Configuration);
            services.AddAutoMapper(GetType().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddCustomSwagger();
            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ValidatorActionFilter));
                opt.EnableEndpointRouting = false;
            })
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.IgnoreNullValues = true;
            })
            .AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<Startup>();
            });


            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddJwt(Configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors(builder =>
               builder
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                );
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwagger(c =>
           {
               c.RouteTemplate = "swagger/{documentName}/swagger.json";
           });

            app.UseSwaggerUI(x =>
           {
               x.SwaggerEndpoint("/swagger/v1/swagger.json", "RealWorld API V1");
           });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
