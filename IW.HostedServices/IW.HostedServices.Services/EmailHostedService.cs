using IW.HostedServices.Core.Models;
using IW.HostedServices.Core.Repositories;
using IW.HostedServices.Core.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IW.HostedServices.Services
{
    public class EmailHostedService : IHostedService, IDisposable
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IEmailService _emailService;
        private int executionCount = 0;
        private long isExecuting = 0;
        private Timer _timer;

        public EmailHostedService(IEmailRepository emailRepository, 
            IEmailService emailService)
        {
            _emailRepository = emailRepository;
            _emailService = emailService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("EmailHostedService is starting.");

            _timer = new Timer(DoWork, cancellationToken, TimeSpan.Zero, 
                TimeSpan.FromSeconds(15));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("EmailHostedService is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var cancellationToken = (CancellationToken)state;
            if (cancellationToken.IsCancellationRequested) return;

            var existingState = Interlocked.Read(ref isExecuting);
            if (!existingState.Equals(0)) return;

            Interlocked.Increment(ref isExecuting);
            var count = Interlocked.Increment(ref executionCount);

            Console.WriteLine($"EmailHostedService is executing run {count}.");
            var processedItems = 0;


            while(!cancellationToken.IsCancellationRequested && 
                _emailRepository.GetNext(out EmailEntry emailEntry))
            {
                // this is where we do the work for each item
                // always wrap each item in a try/catch to prevent the background task from dying
                try
                {
                    await _emailService.SendEmail(emailEntry, cancellationToken);

                    processedItems++;
                }
                catch (Exception)
                { }
            }

            Interlocked.Decrement(ref isExecuting);
            Console.WriteLine($"EmailHostedService is finished processing {processedItems} items.");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
