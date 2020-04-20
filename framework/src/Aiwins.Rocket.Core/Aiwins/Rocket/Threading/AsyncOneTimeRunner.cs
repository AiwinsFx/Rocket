using System;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Aiwins.Rocket.Threading {
    /// <summary>
    /// 用于确保代码块只运行一次.
    /// 它可以被实例化为一个静态对象，以确保代码块在应用程序生命周期中只运行一次。
    /// </summary>
    public class AsyncOneTimeRunner {
        private volatile bool _runBefore;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim (1, 1);

        public async Task RunAsync (Func<Task> action) {
            if (_runBefore) {
                return;
            }

            using (await _semaphore.LockAsync ()) {
                if (_runBefore) {
                    return;
                }

                await action ();

                _runBefore = true;
            }
        }
    }
}