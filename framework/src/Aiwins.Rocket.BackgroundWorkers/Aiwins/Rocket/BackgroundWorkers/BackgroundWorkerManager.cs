using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.BackgroundWorkers {
    /// <summary>
    /// <see cref="IBackgroundWorkerManager"/> 接口的实现
    /// </summary>
    public class BackgroundWorkerManager : IBackgroundWorkerManager, ISingletonDependency, IDisposable {
        protected bool IsRunning { get; private set; }

        private bool _isDisposed;

        private readonly List<IBackgroundWorker> _backgroundWorkers;

        /// <summary>
        /// 初始化创建 <see cref="BackgroundWorkerManager"/> 实例
        /// </summary>
        public BackgroundWorkerManager () {
            _backgroundWorkers = new List<IBackgroundWorker> ();
        }

        public virtual void Add (IBackgroundWorker worker) {
            _backgroundWorkers.Add (worker);

            if (IsRunning) {
                AsyncHelper.RunSync (
                    () => worker.StartAsync ()
                );
            }
        }

        public virtual void Dispose () {
            if (_isDisposed) {
                return;
            }

            _isDisposed = true;

            //TODO: ???
        }

        public virtual async Task StartAsync (CancellationToken cancellationToken = default) {
            IsRunning = true;

            foreach (var worker in _backgroundWorkers) {
                await worker.StartAsync (cancellationToken);
            }
        }

        public virtual async Task StopAsync (CancellationToken cancellationToken = default) {
            IsRunning = false;

            foreach (var worker in _backgroundWorkers) {
                await worker.StopAsync (cancellationToken);
            }
        }
    }
}