using System;
using System.Runtime.Serialization;

namespace Aiwins.Rocket.BackgroundJobs {
    [Serializable]
    public class BackgroundJobExecutionException : RocketException {
        public string JobType { get; set; }

        public object JobArgs { get; set; }

        public BackgroundJobExecutionException () {

        }

        /// <summary>
        /// 创建一个 <see cref="BackgroundJobExecutionException"/> 实例
        /// </summary>
        public BackgroundJobExecutionException (SerializationInfo serializationInfo, StreamingContext context) : base (serializationInfo, context) {

        }

        /// <summary>
        /// 创建一个 <see cref="BackgroundJobExecutionException"/> 实例
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public BackgroundJobExecutionException (string message, Exception innerException) : base (message, innerException) {

        }
    }
}