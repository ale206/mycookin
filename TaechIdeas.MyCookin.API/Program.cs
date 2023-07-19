using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace TaechIdeas.MyCookin.API
{
    public class Program
    {
        /// <summary>
        ///     Early creation of Configuration, so we can set up Serilog ASAP
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host...");

                WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseConfiguration(Configuration)
                    .UseKestrel(c => c.AddServerHeader = false)
                    .CaptureStartupErrors(true) // When true, the host captures exceptions during startup and attempts to start the server.
                    .UseSetting(WebHostDefaults.DetailedErrorsKey, "true") // When enabled (or when the Environment is set to Development), the app captures detailed exceptions.
                    //.UseApplicationInsights()
                    .UseSerilog()
                    .Build()
                    .Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}