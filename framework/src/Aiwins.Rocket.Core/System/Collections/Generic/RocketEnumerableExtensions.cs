using System.Linq;

namespace System.Collections.Generic {
    /// <summary> 
    /// IEnumerable <see cref="IEnumerable{T}"/> 相关的扩展方法。
    /// </summary>
    public static class RocketEnumerableExtensions {
        /// <summary>
        /// 将迭代器中的子项转换为以指定分隔符分割的字符串。
        /// </summary>
        /// <param name="source">IEnumerable集合</param>
        /// <param name="separator">分割符，只有迭代器集合中的子项大于1个才会存在分割符号</param>
        /// <returns>由分隔符字符串分隔的值的成员组成的字符串，如果集合为空，则返回空字符串。</returns>
        public static string JoinAsString (this IEnumerable<string> source, string separator) {
            return string.Join (separator, source);
        }

        /// <summary>
        /// 将迭代器中的子项转换为以指定分隔符分割的字符串。
        /// </summary>
        /// <param name="source">IEnumerable集合</param>
        /// <param name="separator">分割符，只有迭代器集合中的子项大于1个才会存在分割符号</param>
        /// <typeparam name="T">集合成员类型.</typeparam>
        /// <returns>由分隔符字符串分隔的值的成员组成的字符串，如果集合为空，则返回空字符串。</returns>
        public static string JoinAsString<T> (this IEnumerable<T> source, string separator) {
            return string.Join (separator, source);
        }

        /// <summary>
        /// 当条件成立的时候根据指定表达式过滤IEnumerable集合 <see cref="IEnumerable{T}"/> 。
        /// </summary>
        /// <param name="source">IEnumerable集合</param>
        /// <param name="condition">条件是否满足</param>
        /// <param name="predicate">集合过滤表达式</param>
        /// <returns>根据条件 <paramref name="condition"/> 返回对应的IEnumerable集合</returns>
        public static IEnumerable<T> WhereIf<T> (this IEnumerable<T> source, bool condition, Func<T, bool> predicate) {
            return condition ?
                source.Where (predicate) :
                source;
        }

        /// <summary>
        /// 当条件成立的时候根据指定表达式过滤IEnumerable集合 <see cref="IEnumerable{T}"/> 。
        /// </summary>
        /// <param name="source">IEnumerable集合</param>
        /// <param name="condition">条件是否满足</param>
        /// <param name="predicate">集合过滤表达式</param>
        /// <returns>根据条件 <paramref name="condition"/> 返回对应的IEnumerable集合</returns>
        public static IEnumerable<T> WhereIf<T> (this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate) {
            return condition ?
                source.Where (predicate) :
                source;
        }
    }
}