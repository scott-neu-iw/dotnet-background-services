using System;

namespace IW.HostedServices.Core.Models
{
    public class EmailEntry
    {
        public string Subject { get; init; }
        public string Message { get; init; }
        public DateTime CreatedDate { get; init; }

        public override string ToString()
        {
            return $"{CreatedDate}{Environment.NewLine}{Subject}{Environment.NewLine}{Message}";
        }
    }
}
