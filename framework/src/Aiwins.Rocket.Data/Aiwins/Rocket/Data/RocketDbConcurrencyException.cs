using System;

namespace Aiwins.Rocket.Data {
    public class RocketDbConcurrencyException : RocketException {
        /// <summary>
        /// 创建一个数据库并发异常 <see cref="RocketDbConcurrencyException"/> 对象。
        /// </summary>
        public RocketDbConcurrencyException () {

        }

        /// <summary>
        /// 创建一个数据库并发异常 <see cref="RocketDbConcurrencyException"/> 对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        public RocketDbConcurrencyException (string message) : base (message) {

        }

        /// <summary>
        /// 创建一个数据库并发异常 <see cref="RocketDbConcurrencyException"/> 对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常信息</param>
        public RocketDbConcurrencyException (string message, Exception innerException) : base (message, innerException) {

        }
    }
}