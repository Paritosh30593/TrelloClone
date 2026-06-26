using Serilog;
using TC.Infrastructure;
using TC.Application;

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

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            return services;
        }
    }
}