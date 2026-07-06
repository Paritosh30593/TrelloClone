using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using TC.WebAPI.StartupExtensions;

namespace TC.WebAPI
{
    public partial class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureServices(builder);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseExceptionHandlingMiddleware();
            }
            else
            {
                app.UseExceptionHandlingMiddleware();
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}