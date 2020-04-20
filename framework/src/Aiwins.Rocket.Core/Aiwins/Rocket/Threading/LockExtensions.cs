using System;

namespace Aiwins.Rocket.Threading {
    /// <summary>
    /// Lock 相关的扩展方法。
    /// </summary>
    public static class LockExtensions {
        /// <summary>
        /// 通过指定锁 <paramref name="source"/> 在方法 <paramref name="action"/> 执行期间进行锁操作。
        /// </summary>
        /// <param name="source">锁</param>
        /// <param name="action">需锁操作的方法</param>
        public static void Locking (this object source, Action action) {
            lock (source) {
                action ();
            }
        }

        /// <summary>
        /// 通过指定锁 <paramref name="source"/> 在方法 <paramref name="action"/> 执行期间进行锁操作。
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="source">锁</param>
        /// <param name="action">需锁操作的方法</param>
        public static void Locking<T> (this T source, Action<T> action) where T : class {
            lock (source) {
                action (source);
            }
        }

        /// <summary>
        /// 通过指定锁 <paramref name="source"/> 在方法 <paramref name="func"/> 执行期间进行锁操作。
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">锁</param>
        /// <param name="func">需锁操作的方法</param>
        /// <returns>返回方法 <paramref name="func"/> 执行结果</returns>
        public static TResult Locking<TResult> (this object source, Func<TResult> func) {
            lock (source) {
                return func ();
            }
        }

        /// <summary>
        /// 通过指定锁 <paramref name="source"/> 在方法 <paramref name="func"/> 执行期间进行锁操作。
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">锁</param>
        /// <param name="func">需锁操作的方法</param>
        /// <returns>返回方法 <paramref name="func"/> 执行结果</returns>
        public static TResult Locking<T, TResult> (this T source, Func<T, TResult> func) where T : class {
            lock (source) {
                return func (source);
            }
        }
    }
}