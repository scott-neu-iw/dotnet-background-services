using IW.HostedServices.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IW.HostedServices.Core.Services
{
    public interface ILogService
    {
        Task TryLogAsync(LogEntry logEntry, CancellationToken cancellationToken);
    }

    public class LogService : ILogService
    {
        public async Task TryLogAsync(LogEntry logEntry, CancellationToken cancellationToken)
        {
            // force a delay to simulate DB write
            await Task.Delay(2500, cancellationToken);
            Console.WriteLine($"Logged {logEntry}");
        }
    }
}
