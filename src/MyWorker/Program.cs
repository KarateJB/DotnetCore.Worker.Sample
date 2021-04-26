using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWorker.Models.Config;
using MyWorker.Services;
using NLog.Web;

namespace MyWorker
{
    public class Program
    {
        private static string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug($"Program started on {DateTime.Now.ToString()} on env: {environment}.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    // Using options and inject AppSettings
                    services.AddOptions();
                    services.Configure<AppSettings>(hostContext.Configuration);

                    // Service(s) injection
                    services.AddSingleton<ISyncDataService, SyncDataService>();

                    // Register hosted service
                    services.AddHostedService<Worker>();
                })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseNLog();
    }
}
