using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TC.Application.RespositoryContracts;
using TC.Application.RespositoryContracts.Common;
using TC.Infrastructure.DBContext;
using TC.Infrastructure.Repositories;
using TC.Infrastructure.Repositories.Common;

namespace TC.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add infrastructure services here
            var connection = configuration.GetConnectionString("TC");
            services.AddDbContext<TrelloCloneDBContext>(options => options.UseSqlite(connection));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDapperUnitOfWork, DapperUnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            #region Repository
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IColumnRepository, ColumnRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            #endregion
        }
    }
}