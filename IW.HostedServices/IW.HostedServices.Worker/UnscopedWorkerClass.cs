using IW.HostedServices.Core.Services;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace IW.HostedServices.Worker
{
    public class UnscopedWorkerClass : BackgroundService
    {
        private readonly IScopedProcessingService _scopedProcessingService;
        public UnscopedWorkerClass(IScopedProcessingService scopedProcessingService)
        {
            _scopedProcessingService = scopedProcessingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            var executionCount = 10;
            for (var i = 1; i <= executionCount; i++)
            {
                    await Task.Delay(2000, stoppingToken);
                    await _scopedProcessingService.DoWork(stoppingToken);
            }
        }
    }
}
