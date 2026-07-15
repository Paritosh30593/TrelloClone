using Serilog;
using TC.Infrastructure;
using TC.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace TC.WebAPI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
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

            // services.AddAuthentication("def").AddCookie("def");
            // services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}