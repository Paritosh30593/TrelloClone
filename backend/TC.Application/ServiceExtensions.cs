using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TC.Application.Services;
using TC.Application.ServiceContracts;
namespace TC.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add application services here

            #region Services

            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IColumnService, ColumnService>();
            services.AddScoped<ICardService, CardService>();

            #endregion

        }
    }
}