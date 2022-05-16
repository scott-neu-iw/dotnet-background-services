using IW.HostedServices.Core.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IW.HostedServices.Worker
{
    public class EmailSeedWorker : BackgroundService
    {
        private readonly ILogger<EmailSeedWorker> _logger;
        private readonly IEmailRepository _emailRepository;

        public EmailSeedWorker(ILogger<EmailSeedWorker> logger, IEmailRepository emailRepository)
        {
            _logger = logger;
            _emailRepository = emailRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _emailRepository.Add(new Core.Models.EmailEntry { Message = "Service Message", Subject = "Service Subject", CreatedDate = DateTime.Now });
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
