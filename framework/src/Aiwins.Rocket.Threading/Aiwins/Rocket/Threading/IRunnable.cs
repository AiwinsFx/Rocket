using System.Threading;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Threading {
    /// <summary>
    /// 线程服务接口（包含启动和停止操作）
    /// </summary>
    public interface IRunnable {
        /// <summary>
        /// 启动服务
        /// </summary>
        Task StartAsync (CancellationToken cancellationToken = default);

        /// <summary>
        /// 停止服务
        /// </summary>
        Task StopAsync (CancellationToken cancellationToken = default);
    }
}