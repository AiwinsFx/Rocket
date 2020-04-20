using System;
using System.Threading;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.ExceptionHandling;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.Threading {
    /// <summary>
    /// 计时器，按照设置的周期 <see cref="Period"/> 执行应用逻辑
    /// </summary>
    public class RocketTimer : ITransientDependency {
        /// <summary>
        /// 处理程序
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// 计时器周期 (单位:毫秒)
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// 计时器时候启动时执行一次
        /// 默认值: false
        /// </summary>
        public bool RunOnStart { get; set; }

        public ILogger<RocketTimer> Logger { get; set; }

        public IExceptionNotifier ExceptionNotifier { get; set; }

        private readonly Timer _taskTimer;
        private volatile bool _performingTasks;
        private volatile bool _isRunning;

        public RocketTimer () {
            ExceptionNotifier = NullExceptionNotifier.Instance;
            Logger = NullLogger<RocketTimer>.Instance;

            _taskTimer = new Timer (
                TimerCallBack,
                null,
                Timeout.Infinite,
                Timeout.Infinite
            );
        }

        public void Start (CancellationToken cancellationToken = default) {
            if (Period <= 0) {
                throw new RocketException ("Period should be set before starting the timer!");
            }

            lock (_taskTimer) {
                _taskTimer.Change (RunOnStart ? 0 : Period, Timeout.Infinite);
                _isRunning = true;
            }
        }

        public void Stop (CancellationToken cancellationToken = default) {
            lock (_taskTimer) {
                _taskTimer.Change (Timeout.Infinite, Timeout.Infinite);
                while (_performingTasks) {
                    Monitor.Wait (_taskTimer);
                }

                _isRunning = false;
            }
        }

        /// <summary>
        /// _taskTimer调用
        /// </summary>
        /// <param name="state">暂未使用该参数</param>
        private void TimerCallBack (object state) {
            lock (_taskTimer) {
                if (!_isRunning || _performingTasks) {
                    return;
                }

                _taskTimer.Change (Timeout.Infinite, Timeout.Infinite);
                _performingTasks = true;
            }

            try {
                Elapsed.InvokeSafely (this, new EventArgs ());
            } catch (Exception ex) {
                Logger.LogException (ex);
                AsyncHelper.RunSync (() => ExceptionNotifier.NotifyAsync (ex));
            } finally {
                lock (_taskTimer) {
                    _performingTasks = false;
                    if (_isRunning) {
                        _taskTimer.Change (Period, Timeout.Infinite);
                    }

                    Monitor.Pulse (_taskTimer);
                }
            }
        }
    }
}