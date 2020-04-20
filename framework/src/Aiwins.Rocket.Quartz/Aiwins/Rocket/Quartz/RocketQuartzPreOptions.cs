using System;
using System.Collections.Specialized;

namespace Aiwins.Rocket.Quartz {
    public class RocketQuartzPreOptions {
        /// <summary>
        /// 配置参数 Quartz.Impl.StdSchedulerFactory.
        /// </summary>
        public NameValueCollection Properties { get; set; }
        /// <summary>
        /// 等待时长 默认值: 0.
        /// </summary>
        public TimeSpan StartDelay { get; set; }

        public RocketQuartzPreOptions () {
            Properties = new NameValueCollection ();
            StartDelay = new TimeSpan (0);
        }
    }
}