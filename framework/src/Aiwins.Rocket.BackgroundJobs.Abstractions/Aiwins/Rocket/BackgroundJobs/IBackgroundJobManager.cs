using System;
using System.Threading.Tasks;

namespace Aiwins.Rocket.BackgroundJobs {
    /// <summary>
    /// 定义后台作业管理接口
    /// </summary>
    public interface IBackgroundJobManager {
        /// <summary>
        /// 作业入队
        /// </summary>
        /// <typeparam name="TArgs">作业参数类型</typeparam>
        /// <param name="args">作业参数</param>
        /// <param name="priority">作业优先级</param>
        /// <param name="delay">首次执行延时时间</param>
        /// <returns>返回唯一的后台作业</returns>
        Task<string> EnqueueAsync<TArgs> (
            TArgs args,
            BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null
        );
    }
}