using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyWorker.Models.Config;

namespace MyWorker.Services
{
    public class SyncDataService : ISyncDataService
    {
        private readonly ILogger<SyncDataService> _logger;
        private readonly AppSettings _appSettings;

        public SyncDataService(
            ILogger<SyncDataService> logger,
            IOptions<AppSettings> configuration)
        {
            this._logger = logger;
            this._appSettings = configuration.Value;
        }

        public async Task StartAsync()
        {
            this._logger.LogInformation($"Sync from {this._appSettings.SyncRange.StartDate} to {this._appSettings.SyncRange.EndDate}");

            // Start sync data...

            await Task.CompletedTask;
        }
    }
}
