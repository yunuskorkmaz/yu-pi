using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using yu_pi.Helpers;
using yu_pi.Infrastructure.Context;

namespace yu_pi
{
    public static class StartupExtensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["token:signingKey"]));
            var signingCredential = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var issuer = configuration["token:issuer"];
            var audience = configuration["token:audience"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingCredential.Key,
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public static void AddYupiContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<YupiContext>(opt =>
            {
                var connectionString = DatabaseHelper.GetConntectionString(configuration);
                opt.UseNpgsql(connectionString);
            });
        }

        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {   new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()}
                });
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "RealWorld API", Version = "v1" });
                x.CustomSchemaIds(y => y.FullName);
                x.DocInclusionPredicate((version, apiDescription) => true);
                x.TagActionsBy(y => new List<string>()
                {
                    y.GroupName
                });
            });
        }
    }
}