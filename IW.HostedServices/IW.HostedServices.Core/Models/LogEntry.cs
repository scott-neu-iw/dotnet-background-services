using System;

namespace IW.HostedServices.Core.Models
{
    public class LogEntry
    {
        public string User { get; init; }
        public DateTime CreatedDate { get; init; }
        public string Message { get; init; }
        public int Severity { get; init; }

        public override string ToString()
        {
            return $"{CreatedDate}: Severity: {Severity}, User: {User}, {Message}";
        }
    }
}
