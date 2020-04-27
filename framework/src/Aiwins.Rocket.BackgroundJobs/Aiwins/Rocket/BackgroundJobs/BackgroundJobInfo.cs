using System;

namespace Aiwins.Rocket.BackgroundJobs {
    /// <summary>
    /// 可用于持久化的后台作业信息
    /// </summary>
    public class BackgroundJobInfo {
        public Guid Id { get; set; }

        /// <summary>
        /// 作业名称
        /// </summary>
        public virtual string JobName { get; set; }

        /// <summary>
        /// 作业参数
        /// </summary>
        public virtual string JobArgs { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public virtual short TryCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 下次重试时间
        /// </summary>
        public virtual DateTime NextTryTime { get; set; }

        /// <summary>
        /// 最后一次重试时间
        /// </summary>
        public virtual DateTime? LastTryTime { get; set; }

        /// <summary>
        /// 如果作业连续失败且不会再次执行，则设置为true
        /// </summary>
        public virtual bool IsAbandoned { get; set; }

        /// <summary>
        /// 作业优先级
        /// </summary>
        public virtual BackgroundJobPriority Priority { get; set; }

        /// <summary>
        /// 创建一个作业 <see cref="BackgroundJobInfo"/> 实例
        /// </summary>
        public BackgroundJobInfo () {
            Priority = BackgroundJobPriority.Normal;
        }
    }
}