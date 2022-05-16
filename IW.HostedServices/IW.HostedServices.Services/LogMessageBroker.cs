using IW.HostedServices.Core.Models;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IW.HostedServices.Services
{
    public interface ILogMessageBroker
    {
        ChannelReader<LogEntry> Reader { get; init; }

        Task WriteAsync(LogEntry item);
    }

    public class LogMessageBroker : ILogMessageBroker
    {
        public ChannelReader<LogEntry> Reader { get; init; }
        private readonly ChannelWriter<LogEntry> _writer;

        public LogMessageBroker(ChannelWriter<LogEntry> writer, ChannelReader<LogEntry> reader)
        {
            Reader = reader;
            _writer = writer;
        }

        public async Task WriteAsync(LogEntry item)
        {
            await _writer.WriteAsync(item);
        }
    }
}
