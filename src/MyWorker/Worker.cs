using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyWorker.Models.Config;
using MyWorker.Services;

namespace MyWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AppSettings _appSettings;
        private readonly ISyncDataService _syncDataService;

        public Worker(
            ILogger<Worker> logger,
            IOptions<AppSettings> configuration,
            ISyncDataService syncDataService)
        {
            this._logger = logger;
            this._appSettings = configuration.Value;
            this._syncDataService = syncDataService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this._logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // Business logic
                await this._syncDataService.StartAsync();

                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
