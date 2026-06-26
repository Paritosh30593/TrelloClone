using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TC.Application.ServiceContracts.BoardAggregate;
using TC.Application.ServiceContracts.CardAggregate;
using TC.Application.Services;
using TC.Application.ServiceContracts.ColumnAggregate;
namespace TC.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add application services here

            #region Services

            #region Board Services
            services.AddScoped<IBoardAdderService, BoardService>();
            services.AddScoped<IBoardDeleterService, BoardService>();
            services.AddScoped<IBoardUpdaterService, BoardService>();
            services.AddScoped<IBoardGetterService, BoardService>();
            #endregion

            #region Card Services
            services.AddScoped<ICardAdderService, CardService>();
            services.AddScoped<ICardDeleterService, CardService>();
            services.AddScoped<ICardUpdaterService, CardService>();
            services.AddScoped<ICardGetterService, CardService>();
            #endregion

            #region Column Services
            services.AddScoped<IColumnAdderService, ColumnService>();
            services.AddScoped<IColumnDeleterService, ColumnService>();
            services.AddScoped<IColumnUpdaterService, ColumnService>();
            services.AddScoped<IColumnGetterService, ColumnService>();
            #endregion

            #endregion

        }
    }
}