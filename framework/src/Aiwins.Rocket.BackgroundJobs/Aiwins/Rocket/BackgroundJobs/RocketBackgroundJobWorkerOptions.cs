namespace Aiwins.Rocket.BackgroundJobs {
    public class RocketBackgroundJobWorkerOptions {
        /// <summary>
        /// 作业存储 <see cref="IBackgroundJobStore"/> 之间的轮询时间间隔
        /// 默认值: 5000 (5 秒).
        /// </summary>
        public int JobPollPeriod { get; set; }

        /// <summary>
        /// 一次循环中要从数据存储中获取的最大作业数量
        /// 默认值: 1000.
        /// </summary>
        public int MaxJobFetchCount { get; set; }

        /// <summary>
        /// 首次执行失败的间隔时间 (单位：秒)
        /// 默认值: 60 (1分钟).
        /// </summary>
        public int DefaultFirstWaitDuration { get; set; }

        /// <summary>
        /// 作业终止执行 (<see cref="BackgroundJobInfo.IsAbandoned"/>) 的超时时间 (单位：秒) 
        /// 默认值: 172,800 (2天).
        /// </summary>
        public int DefaultTimeout { get; set; }

        /// <summary>
        /// 执行失败等待时间间隔
        /// 此值乘以上次等待时间以计算下次等待时间。
        /// 默认值: 2.0.
        /// </summary>
        public double DefaultWaitFactor { get; set; }

        public RocketBackgroundJobWorkerOptions () {
            MaxJobFetchCount = 1000;
            JobPollPeriod = 5000;
            DefaultFirstWaitDuration = 60;
            DefaultTimeout = 172800;
            DefaultWaitFactor = 2.0;
        }
    }
}