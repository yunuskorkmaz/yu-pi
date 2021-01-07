using System;
using Microsoft.Extensions.Configuration;

namespace Api.Helpers
{
    public static class DatabaseHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string connStr;
            string connUrl;
            var connTemplate = "{0}";
            if (env == "Development")
            {
                connUrl = configuration.GetConnectionString("Default");
                connTemplate = "{0};sslmode=Prefer;Trust Server Certificate=true";
            }
            else
            {
                connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            }
            if (!String.IsNullOrEmpty(connUrl))
            {
                connUrl = connUrl.Replace("postgres://", string.Empty);

                var pgUserPass = connUrl.Split("@")[0];
                var pgHostPortDb = connUrl.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];

                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPass = pgUserPass.Split(":")[1];
                var pgHost = pgHostPort.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];

                connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}";
                connStr = String.Format(connTemplate, connStr);
                return connStr;
            }
            return "";
        }
    }
}