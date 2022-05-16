using IW.HostedServices.Core.Models;
using IW.HostedServices.Core.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IW.HostedServices.Services
{
    public class LogHostedService : BackgroundService
    {
        private readonly ILogService _logger;
        private readonly ChannelReader<LogEntry> _channel;
        public LogHostedService(ILogService logService, ChannelReader<LogEntry> channel)
        {
            _logger = logService;
            _channel = channel;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await foreach (var item in _channel.ReadAllAsync(cancellationToken))
            {
                try
                {
                    await _logger.TryLogAsync(item, cancellationToken);
                    
                }
                catch (Exception)
                {
                    // handle exceptions
                }
            }
        }
    }
}
