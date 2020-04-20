using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.BackgroundJobs.Hangfire {
    public class HangfireJobExecutionAdapter<TArgs> {
        protected RocketBackgroundJobOptions Options { get; }
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected IBackgroundJobExecuter JobExecuter { get; }

        public HangfireJobExecutionAdapter (
            IOptions<RocketBackgroundJobOptions> options,
            IBackgroundJobExecuter jobExecuter,
            IServiceScopeFactory serviceScopeFactory) {
            JobExecuter = jobExecuter;
            ServiceScopeFactory = serviceScopeFactory;
            Options = options.Value;
        }

        public void Execute (TArgs args) {
            using (var scope = ServiceScopeFactory.CreateScope ()) {
                var jobType = Options.GetJob (typeof (TArgs)).JobType;
                var context = new JobExecutionContext (scope.ServiceProvider, jobType, args);
                AsyncHelper.RunSync (() => JobExecuter.ExecuteAsync (context));
            }
        }
    }
}