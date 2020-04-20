using System;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.ExceptionHandling;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket.BackgroundWorkers {
    public abstract class AsyncPeriodicBackgroundWorkerBase : BackgroundWorkerBase {
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected RocketTimer Timer { get; }

        protected AsyncPeriodicBackgroundWorkerBase (
            RocketTimer timer,
            IServiceScopeFactory serviceScopeFactory) {
            ServiceScopeFactory = serviceScopeFactory;
            Timer = timer;
            Timer.Elapsed += Timer_Elapsed;
        }

        public override async Task StartAsync (CancellationToken cancellationToken = default) {
            await base.StartAsync (cancellationToken);
            Timer.Start (cancellationToken);
        }

        public override async Task StopAsync (CancellationToken cancellationToken = default) {
            Timer.Stop (cancellationToken);
            await base.StopAsync (cancellationToken);
        }

        private void Timer_Elapsed (object sender, System.EventArgs e) {
            using (var scope = ServiceScopeFactory.CreateScope ()) {
                try {
                    AsyncHelper.RunSync (
                        () => DoWorkAsync (new PeriodicBackgroundWorkerContext (scope.ServiceProvider))
                    );
                } catch (Exception ex) {
                    AsyncHelper.RunSync (
                        () => scope.ServiceProvider
                        .GetRequiredService<IExceptionNotifier> ()
                        .NotifyAsync (new ExceptionNotificationContext (ex))
                    );

                    Logger.LogException (ex);
                }
            }
        }

        protected abstract Task DoWorkAsync (PeriodicBackgroundWorkerContext workerContext);
    }
}