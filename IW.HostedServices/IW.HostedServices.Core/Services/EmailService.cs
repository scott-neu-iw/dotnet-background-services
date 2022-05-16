using IW.HostedServices.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IW.HostedServices.Core.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailEntry emailEntry, CancellationToken cancellationToken);
    }

    public class EmailService : IEmailService
    {
        public async Task SendEmail(EmailEntry emailEntry, CancellationToken cancellationToken)
        {
            // force a delay to simulate DB write
            await Task.Delay(2500, cancellationToken);
            Console.WriteLine($"Emailing{Environment.NewLine}{emailEntry}");
        }
    }
}
