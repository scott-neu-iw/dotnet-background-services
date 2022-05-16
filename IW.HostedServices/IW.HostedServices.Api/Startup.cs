using IW.HostedServices.Services;
using IW.HostedServices.Core.Models;
using IW.HostedServices.Core.Repositories;
using IW.HostedServices.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;

namespace IW.HostedServices.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // OPTION 1: Fire & Forget using channels
            services.AddSingleton(Channel.CreateUnbounded<LogEntry>
                (new UnboundedChannelOptions() { SingleReader = true }));
            services.AddSingleton(svc => svc.GetRequiredService<Channel<LogEntry>>().Reader);
            services.AddSingleton(svc => svc.GetRequiredService<Channel<LogEntry>>().Writer);

            // worker service
            services.AddSingleton<ILogService, LogService>();

            // hosted service
            services.AddHostedService<LogHostedService>();


            //// OPTION 2: Timer based service
            //services.AddSingleton<IEmailRepository, EmailRepository>();

            //// add worker service
            //services.AddSingleton<IEmailService, EmailService>();

            //// hosted service
            //services.AddHostedService<EmailHostedService>();


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
