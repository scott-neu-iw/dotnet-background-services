using IW.HostedServices.Core.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace IW.HostedServices.Core.Repositories
{
    public interface IEmailRepository
    {
        bool GetNext(out EmailEntry emailEntry);
        Task Add(EmailEntry emailEntry);
    }

    public class EmailRepository : IEmailRepository
    {
        public ConcurrentQueue<EmailEntry> _simulatedDb = new();

        public async Task Add(EmailEntry emailEntry)
        {
            await Task.Run(() => _simulatedDb.Enqueue(emailEntry));
        }

        public bool GetNext(out EmailEntry emailEntry)
        {
            if (_simulatedDb.TryDequeue(out EmailEntry item))
            {
                emailEntry = item;
                return true;
            }
            emailEntry = null;
            return false;
        }
    }
}
