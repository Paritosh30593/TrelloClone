using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TC.WebAPI.StartupExtensions
{
    public static class ConfigureApplicationsEntensions
    {
        public static void ConfigureApplications(this WebApplication app)
        {
            // Add custom application configuration here
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