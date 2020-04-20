using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.BackgroundWorkers {
    /// <summary>
    /// 在后台运行以执行某些任务的工作线程的接口
    /// </summary>
    public interface IBackgroundWorker : IRunnable, ISingletonDependency {

    }
}