using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiwins.Rocket.BackgroundJobs {
    /// <summary>
    /// 定义后台作业存储
    /// </summary>
    public interface IBackgroundJobStore {
        /// <summary>
        /// 通过jobId获取作业信息
        /// </summary>
        /// <param name="jobId">作业的唯一标识</param>
        /// <returns>作业实例</returns>
        Task<BackgroundJobInfo> FindAsync (Guid jobId);

        /// <summary>
        /// 添加作业
        /// </summary>
        /// <param name="jobInfo">作业信息</param>
        Task InsertAsync (BackgroundJobInfo jobInfo);

        /// <summary>
        /// 获取等待的作业
        /// 查询条件: !IsAbandoned And NextTryTime &lt;= Clock.Now.
        /// 排序方式: Priority DESC, TryCount ASC, NextTryTime ASC.
        /// 最大结果数量: <paramref name="maxResultCount"/>.
        /// </summary>
        /// <param name="maxResultCount">最大结果集数量</param>
        Task<List<BackgroundJobInfo>> GetWaitingJobsAsync (int maxResultCount);

        /// <summary>
        /// 删除作业
        /// </summary>
        /// <param name="jobId">作业唯一标识</param>
        Task DeleteAsync (Guid jobId);

        /// <summary>
        /// 更新作业
        /// </summary>
        /// <param name="jobInfo">作业信息</param>
        Task UpdateAsync (BackgroundJobInfo jobInfo);
    }
}