using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;
using Quartz;

namespace Aiwins.Rocket.BackgroundWorkers.Quartz {
    [Dependency (ReplaceServices = true)]
    public class QuartzBackgroundWorkerManager : IBackgroundWorkerManager, ISingletonDependency {
        private readonly IScheduler _scheduler;

        public QuartzBackgroundWorkerManager (IScheduler scheduler) {
            _scheduler = scheduler;
        }

        public async Task StartAsync (CancellationToken cancellationToken = default) {
            await _scheduler.ResumeAll (cancellationToken);
        }

        public async Task StopAsync (CancellationToken cancellationToken = default) {
            if (!_scheduler.IsShutdown) {
                await _scheduler.PauseAll (cancellationToken);
            }
        }

        public void Add (IBackgroundWorker worker) {
            if (worker is IQuartzBackgroundWorker quartzWork) {
                Check.NotNull (quartzWork.Trigger, nameof (quartzWork.Trigger));
                Check.NotNull (quartzWork.JobDetail, nameof (quartzWork.JobDetail));

                AsyncHelper.RunSync (() => _scheduler.ScheduleJob (quartzWork.JobDetail, quartzWork.Trigger));
            }
        }
    }
}