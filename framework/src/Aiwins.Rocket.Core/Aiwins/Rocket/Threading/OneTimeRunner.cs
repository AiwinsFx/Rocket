using System;

namespace Aiwins.Rocket.Threading {
    /// <summary>
    /// 用于确保代码块只运行一次.
    /// 它可以被实例化为一个静态对象，以确保代码块在应用程序生命周期中只运行一次。
    /// </summary>
    public class OneTimeRunner {
        private volatile bool _runBefore;

        public void Run (Action action) {
            if (_runBefore) {
                return;
            }

            lock (this) {
                if (_runBefore) {
                    return;
                }

                action ();

                _runBefore = true;
            }
        }
    }
}