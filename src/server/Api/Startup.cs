using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Extensions;
using Api.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App.Api
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
            services.AddHttpClient();
            services.AddControllersWithValidationFilter().AddValidation();
            services.AddSpaStaticFiles(option =>
            {
                option.RootPath = "wwwroot";
            });
            services.AddAppContext(Configuration);
            services.AddSwagger();
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddJwtAuth(Configuration);
            services.AddAppDependencyInjection();

            services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();

                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AMS API V1"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {

                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "../../client";
                    spa.UseReactDevelopmentServer(npmScript: "start");
                    // spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
                else
                {
                    spa.Options.SourcePath = "wwwroot";
                }
            });
        }
    }
}
