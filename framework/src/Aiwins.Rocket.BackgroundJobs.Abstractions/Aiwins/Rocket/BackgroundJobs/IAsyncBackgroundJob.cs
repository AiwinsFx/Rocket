using System.Threading.Tasks;

namespace Aiwins.Rocket.BackgroundJobs {
    /// <summary>
    /// 定义后台作业的接口
    /// </summary>
    public interface IAsyncBackgroundJob<in TArgs> {
        /// <summary>
        /// 传入参数 <see cref="args"/> 提交作业
        /// </summary>
        /// <param name="args">作业参数</param>
        Task ExecuteAsync (TArgs args);
    }
}