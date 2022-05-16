using IW.HostedServices.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IW.HostedServices.Worker
{
    public class ScopedWorkerClass : BackgroundService
    {
        private readonly IServiceProvider _services;
        public ScopedWorkerClass(IServiceProvider services)
        {
            _services = services;
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
                using (var scope = _services.CreateScope())
                {
                    await Task.Delay(2000, stoppingToken);

                    var scopedProcessingService = scope.ServiceProvider
                            .GetRequiredService<IScopedProcessingService>();

                    await scopedProcessingService.DoWork(stoppingToken);
                }
            }
        }
    }
}
