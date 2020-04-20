using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.Quartz {
    public class RocketQuartzModule : RocketModule {
        private IScheduler _scheduler;

        public override void ConfigureServices (ServiceConfigurationContext context) {
            var options = context.Services.ExecutePreConfiguredActions<RocketQuartzPreOptions> ();
            context.Services.AddSingleton (AsyncHelper.RunSync (() => new StdSchedulerFactory (options.Properties).GetScheduler ()));
            context.Services.AddSingleton (typeof (IJobFactory), typeof (RocketQuartzJobFactory));
        }

        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            var options = context.ServiceProvider.GetRequiredService<IOptions<RocketQuartzPreOptions>> ().Value;

            _scheduler = context.ServiceProvider.GetService<IScheduler> ();
            _scheduler.JobFactory = context.ServiceProvider.GetService<IJobFactory> ();

            if (options.StartDelay.Ticks > 0) {
                AsyncHelper.RunSync (() => _scheduler.StartDelayed (options.StartDelay));
            } else {
                AsyncHelper.RunSync (() => _scheduler.Start ());
            }
        }

        public override void OnApplicationShutdown (ApplicationShutdownContext context) {
            //TODO: 考虑提供两种关闭应用程序的方法: OnPreApplicationShutdown & OnApplicationShutdown
            AsyncHelper.RunSync (() => _scheduler.Shutdown ());
        }
    }
}