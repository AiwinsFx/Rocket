using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.BackgroundWorkers {
    /// <summary>
    /// 用于管理后台工作
    /// </summary>
    public interface IBackgroundWorkerManager : IRunnable {
        /// <summary>
        /// 添加后台工作
        /// </summary>
        /// <param name="worker">
        /// 从IOC容器解析的后台工作
        /// </param>
        void Add (IBackgroundWorker worker);
    }
}