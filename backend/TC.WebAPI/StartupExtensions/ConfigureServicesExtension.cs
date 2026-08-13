using Serilog;
using TC.Infrastructure;
using TC.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;

namespace TC.WebAPI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            string tenantId = "61ef8618-c01d-4665-ba49-e4601de7e685";
            string appId = "ca455e05-f78e-4eb5-ad38-cd56a3299f42";

            // Add custom services here
            builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
            {
                loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .ReadFrom.Services(services);
            });

            services.ConfigureInfrastructureServices(builder.Configuration);
            services.ConfigureApplicationServices(builder.Configuration);

            services.AddControllers();
            services.AddOpenApi();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuers =
                        [
                            $"https://sts.windows.net/{tenantId}/",
                            $"https://login.microsoftonline.com/{tenantId}/v2.0"
                        ],
                        ValidateAudience = true,
                        ValidAudiences =
                        [
                            appId,
                            $"api://{appId}"
                        ]
                    };
                });

            services.AddAuthorizationBuilder();

            string[] corsOrigins = builder
                .Configuration["CorsOrigins"]
                ?.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(corsOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}