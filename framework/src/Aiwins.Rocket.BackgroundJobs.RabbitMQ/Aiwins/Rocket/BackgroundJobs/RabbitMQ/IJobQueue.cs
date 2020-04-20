using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.BackgroundJobs.RabbitMQ {
    public interface IJobQueue<in TArgs> : IRunnable, IDisposable {
        Task<string> EnqueueAsync (
            TArgs args,
            BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null
        );
    }
}