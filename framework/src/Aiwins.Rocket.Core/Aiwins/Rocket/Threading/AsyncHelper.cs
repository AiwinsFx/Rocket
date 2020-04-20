using System;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nito.AsyncEx;

namespace Aiwins.Rocket.Threading {
    /// <summary>
    /// Async帮助类，
    /// 提供了许多实用方法.
    /// </summary>
    public static class AsyncHelper {
        /// <summary>
        /// 判断方法是否为异步方法
        /// </summary>
        /// <param name="method">A method to check</param>
        public static bool IsAsync ([NotNull] this MethodInfo method) {
            Check.NotNull (method, nameof (method));

            return method.ReturnType.IsTaskOrTaskOfT ();
        }

        public static bool IsTaskOrTaskOfT ([NotNull] this Type type) {
            return type == typeof (Task) || (type.GetTypeInfo ().IsGenericType && type.GetGenericTypeDefinition () == typeof (Task<>));
        }

        public static bool IsTaskOfT ([NotNull] this Type type) {
            return type.GetTypeInfo ().IsGenericType && type.GetGenericTypeDefinition () == typeof (Task<>);
        }

        public static Type UnwrapTask ([NotNull] Type type) {
            Check.NotNull (type, nameof (type));

            if (type == typeof (Task)) {
                return typeof (void);
            }

            if (type.IsTaskOfT ()) {
                return type.GenericTypeArguments[0];
            }

            return type;
        }

        /// <summary>
        /// 同步的方式运行异步方法。
        /// </summary>
        /// <param name="func">异步方法</param>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <returns>异步操作的结果</returns>
        public static TResult RunSync<TResult> (Func<Task<TResult>> func) {
            return AsyncContext.Run (func);
        }

        /// <summary>
        /// 同步的方式运行异步方法。
        /// </summary>
        /// <param name="action">异步方法</param>
        public static void RunSync (Func<Task> action) {
            AsyncContext.Run (action);
        }
    }
}