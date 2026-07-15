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
            // Create a WebApplicationBuilder instance
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Services.ConfigureServices(builder);

            // Build the WebApplication instance
            WebApplication app = builder.Build();
            app.ConfigureApplications();
        }
    }
}