using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aiwins.Rocket.Caching {
    public static class CacheKeyHelper {
        /// <summary>
        /// 键值分隔符.
        /// </summary>
        private static char _linkChar = ':';

        /// <summary>
        /// 获取键.
        /// </summary>
        /// <returns>键.</returns>
        /// <param name="prefix">前缀.</param>
        /// <param name="value">值.</param>
        public static string GetCacheKey (string prefix, string value) {
            return string.Concat (prefix.Trim (), _linkChar, value.Trim ()).TrimStart (_linkChar);
        }

        /// <summary>
        /// 获取键.
        /// </summary>
        /// <returns>键.</returns>
        /// <param name="scope">作用域.</param>
        /// <param name="prefix">前缀.</param>
        /// <param name="value">值.</param>
        public static string GetCacheKey (string scope, string prefix, string value) {
            return string.Concat (scope.Trim (), _linkChar, GetCacheKey (prefix, value)).TrimStart (_linkChar);
        }

        /// <summary>
        /// 获取键.
        /// </summary>
        /// <returns>键.</returns>
        /// <param name="methodInfo">方法信息.</param>
        /// <param name="args">方法参数.</param>
        /// <param name="prefix">前缀.</param>
        public static string GetCacheKey (MethodInfo methodInfo, object[] args, string prefix) {
            if (string.IsNullOrWhiteSpace (prefix)) {
                var typeName = methodInfo.DeclaringType.Name;
                var methodName = methodInfo.Name;

                var methodArguments = FormatArgumentsToPartOfCacheKey (args);

                return GenerateCacheKey (typeName, methodName, methodArguments);
            } else {
                var methodArguments = FormatArgumentsToPartOfCacheKey (args);

                return GenerateCacheKey (string.Empty, prefix, methodArguments);
            }
        }

        /// <summary>
        /// 生成缓存键前缀
        /// </summary>
        /// <returns>键前缀.</returns>
        /// <param name="methodInfo">方法信息.</param>
        /// <param name="prefix">字符串前缀.</param>
        public static string GetCacheKeyPrefix (MethodInfo methodInfo, string prefix) {
            if (string.IsNullOrWhiteSpace (prefix)) {
                var typeName = methodInfo.DeclaringType.Name;
                var methodName = methodInfo.Name;

                return GenerateCacheKeyPrefix (typeName, methodName);
            } else {
                return GenerateCacheKeyPrefix (string.Empty, prefix);
            }
        }

        /// <summary>
        /// 生成缓存键前缀.
        /// </summary>
        /// <returns>键前缀.</returns>
        /// <param name="first">First.</param>
        /// <param name="second">Second.</param>
        private static string GenerateCacheKeyPrefix (string first, string second) {
            return string.Concat (first, _linkChar, second, _linkChar).TrimStart (_linkChar);
        }

        /// <summary>
        /// 格式化键值.
        /// </summary>
        /// <returns>方法参数.</returns>
        /// <param name="methodArguments">方法参数列表</param>
        private static IList<string> FormatArgumentsToPartOfCacheKey (object[] methodArguments) {
            if (methodArguments != null && methodArguments.Length > 0) {
                return methodArguments.Select (GetArgumentValue).ToList ();
            } else {
                return new List<string> { "0" };
            }
        }

        /// <summary>
        /// 生成缓存键.
        /// </summary>
        /// <returns>The cache key.</returns>
        /// <param name="typeName">Type name.</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="parameters">Parameters.</param>
        private static string GenerateCacheKey (string typeName, string methodName, IList<string> parameters) {
            var builder = new StringBuilder ();

            builder.Append (GenerateCacheKeyPrefix (typeName, methodName));

            foreach (var param in parameters) {
                builder.Append (param);
                builder.Append (_linkChar);
            }

            var str = builder.ToString ().TrimEnd (_linkChar);

            return str;
        }

        /// <summary>
        /// 获取参数值.
        /// </summary>
        /// <returns>The argument value.</returns>
        /// <param name="arg">Argument.</param>
        private static string GetArgumentValue (object arg) {
            if (arg is int || arg is long || arg is Guid || arg is int? || arg is long? || arg is Guid? || arg is Enum || arg is string)
                return arg.ToString ();

            if (arg is DateTime || arg is DateTimeOffset || arg is DateTime? || arg is DateTimeOffset?)
                return ((DateTime) arg).ToString ("yyyyMMddHHmmss");

            return null;
        }
    }
}