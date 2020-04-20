using Quartz;

namespace Aiwins.Rocket.BackgroundWorkers.Quartz {
    public interface IQuartzBackgroundWorker : IBackgroundWorker, IJob {
        ITrigger Trigger { get; set; }

        IJobDetail JobDetail { get; set; }
    }
}