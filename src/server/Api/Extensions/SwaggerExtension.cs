using System;
using System.IO;
using System.Reflection;
using Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme{
                    Name = "JWT Auth",
                    Description= "Enter JWT Bearer Token only",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference{
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.OperationFilter<AuthHeaderOperationFilter>();
                c.AddSecurityDefinition(securityScheme.Reference.Id,securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {securityScheme,new string[]{}}
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}