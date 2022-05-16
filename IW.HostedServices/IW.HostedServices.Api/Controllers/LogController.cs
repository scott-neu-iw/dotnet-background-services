using IW.HostedServices.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IW.HostedServices.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController: ControllerBase
    {
        private readonly ChannelWriter<LogEntry> _channel;
        public LogController(ChannelWriter<LogEntry> channel)
        {
            _channel = channel;
        }

        [HttpPost()]
        public async Task<IActionResult> Post()
        {
            await _channel.WriteAsync(new LogEntry
            {
                Severity = 1,
                Message = "Test Message",
                User = "DemoUser",
                CreatedDate = DateTime.Now
            });

            return Ok();
        }
    }
}
