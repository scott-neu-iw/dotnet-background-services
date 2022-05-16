using System;
using System.Threading;
using System.Threading.Tasks;

namespace IW.HostedServices.Core.Services
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    public class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;

        public async Task DoWork(CancellationToken stoppingToken)
        {
            executionCount++;

            Console.WriteLine($"Executing ScopedProcessingService, count {executionCount}");
        }
    }
}
