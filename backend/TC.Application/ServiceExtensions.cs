using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TC.Application.Helpers;
using TC.Application.ServiceContracts;
using TC.Application.Services;

namespace TC.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Options

            services.Configure<DefaultColumnsOptions>(
                configuration.GetSection(DefaultColumnsOptions.SectionName));
            services.Configure<DefaultCardsOptions>(
                configuration.GetSection(DefaultCardsOptions.SectionName));

            #endregion

            #region Services

            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IColumnService, ColumnService>();
            services.AddScoped<ICardService, CardService>();

            #endregion
        }
    }
}