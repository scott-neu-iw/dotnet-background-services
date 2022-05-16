using IW.HostedServices.Core.Repositories;
using IW.HostedServices.Core.Services;
using IW.HostedServices.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IW.HostedServices.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "IW.HostedService";
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // REUSE BACKGROUND SERVICE
                    services.AddSingleton<IEmailRepository, EmailRepository>();
                    services.AddSingleton<IEmailService, EmailService>();

                    services.AddHostedService<EmailSeedWorker>();
                    services.AddHostedService<EmailHostedService>();

                    // SCOPED PROCESSING
                    //services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
                    //services.AddHostedService<UnscopedWorkerClass>();

                    //services.AddSingleton<IScopedProcessingService, ScopedProcessingService>();
                    //services.AddHostedService<UnscopedWorkerClass>();

                    //services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
                    //services.AddHostedService<ScopedWorkerClass>();
                });
    }
}
