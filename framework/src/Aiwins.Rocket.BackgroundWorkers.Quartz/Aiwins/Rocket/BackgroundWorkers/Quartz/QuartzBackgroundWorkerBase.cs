using System.Threading.Tasks;
using Quartz;

namespace Aiwins.Rocket.BackgroundWorkers.Quartz {
    public abstract class QuartzBackgroundWorkerBase : BackgroundWorkerBase, IQuartzBackgroundWorker {
        public ITrigger Trigger { get; set; }

        public IJobDetail JobDetail { get; set; }

        public abstract Task Execute (IJobExecutionContext context);
    }
}