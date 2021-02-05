using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddDbContext<YupiContext>(opt => {
                opt.UseNpgsql("");
            });
        }
    }
}