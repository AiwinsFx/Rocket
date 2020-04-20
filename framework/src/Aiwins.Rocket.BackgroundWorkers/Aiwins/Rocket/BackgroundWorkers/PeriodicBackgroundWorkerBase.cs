using System;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.ExceptionHandling;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket.BackgroundWorkers {
    /// <summary>
    /// 继承自 <see cref="BackgroundWorkerBase"/> 基类，实现后台作业定期运行 
    /// </summary>
    public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase {
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected RocketTimer Timer { get; }

        protected PeriodicBackgroundWorkerBase (
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

                    DoWork (new PeriodicBackgroundWorkerContext (scope.ServiceProvider));
                } catch (Exception ex) {
                    scope.ServiceProvider
                        .GetRequiredService<IExceptionNotifier> ()
                        .NotifyAsync (new ExceptionNotificationContext (ex));

                    Logger.LogException (ex);
                }
            }
        }

        /// <summary>
        /// 调用
        /// </summary>
        protected abstract void DoWork (PeriodicBackgroundWorkerContext workerContext);
    }
}