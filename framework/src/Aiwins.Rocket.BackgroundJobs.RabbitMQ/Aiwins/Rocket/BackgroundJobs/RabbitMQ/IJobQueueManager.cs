using System.Threading.Tasks;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.BackgroundJobs.RabbitMQ {
    public interface IJobQueueManager : IRunnable {
        Task<IJobQueue<TArgs>> GetAsync<TArgs> ();
    }
}