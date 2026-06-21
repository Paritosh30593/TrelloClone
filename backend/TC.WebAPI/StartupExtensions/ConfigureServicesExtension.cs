using Microsoft.EntityFrameworkCore;
using Serilog;
using TC.Infrastructure.DBContext;

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

            services.AddDbContext<TrelloCloneDBContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("TC"));
            });

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            return services;
        }
    }
}