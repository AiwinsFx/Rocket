using System.Linq;
using Aiwins.Rocket;
using JetBrains.Annotations;

namespace System.Collections.Generic {
    /// <summary>
    /// ICollection <see cref="ICollection{T}"/> 相关的扩展方法。
    /// </summary>
    public static class RocketCollectionExtensions {
        /// <summary>
        /// 检查ICollection集合是否为空。
        /// </summary>
        public static bool IsNullOrEmpty<T> ([CanBeNull] this ICollection<T> source) {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 如果子项不存在，则添加至ICollection集合。
        /// </summary>
        /// <param name="source">ICollection集合</param>
        /// <param name="item">子项</param>
        /// <typeparam name="T">集合成员类型</typeparam>
        /// <returns>添加至ICollection集合返回true，否则返回false。</returns>
        public static bool AddIfNotContains<T> ([NotNull] this ICollection<T> source, T item) {
            Check.NotNull (source, nameof (source));

            if (source.Contains (item)) {
                return false;
            }

            source.Add (item);
            return true;
        }

        /// <summary>
        /// 如果子集不存在，则添加至ICollection集合。
        /// </summary>
        /// <param name="source">ICollection集合</param>
        /// <param name="items">子集</param>
        /// <typeparam name="T">集合成员类型</typeparam>
        /// <returns>子集。</returns>
        public static IEnumerable<T> AddIfNotContains<T> ([NotNull] this ICollection<T> source, IEnumerable<T> items) {
            Check.NotNull (source, nameof (source));

            var addedItems = new List<T> ();

            foreach (var item in items) {
                if (source.Contains (item)) {
                    continue;
                }

                source.Add (item);
                addedItems.Add (item);
            }

            return addedItems;
        }

        /// <summary>
        /// 如果满足表达式 <paramref name="predicate"/> 的子项不存在，则添加至ICollection集合。
        /// </summary>
        /// <param name="source">ICollection集合</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="itemFactory">工厂，返回待添加至集合的子项</param>
        /// <typeparam name="T">集合成员类型</typeparam>
        /// <returns>添加至ICollection集合返回true，否则返回false。</returns>
        public static bool AddIfNotContains<T> ([NotNull] this ICollection<T> source, [NotNull] Func<T, bool> predicate, [NotNull] Func<T> itemFactory) {
            Check.NotNull (source, nameof (source));
            Check.NotNull (predicate, nameof (predicate));
            Check.NotNull (itemFactory, nameof (itemFactory));

            if (source.Any (predicate)) {
                return false;
            }

            source.Add (itemFactory ());
            return true;
        }

        /// <summary>
        /// 移除ICollection集合中满足表达式 <paramref name="predicate"/> 的子集。
        /// </summary>
        /// <typeparam name="T">集合成员类型</typeparam>
        /// <param name="source">ICollection集合</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns>移除的子集</returns>
        public static IList<T> RemoveAll<T> ([NotNull] this ICollection<T> source, Func<T, bool> predicate) {
            var items = source.Where (predicate).ToList ();

            foreach (var item in items) {
                source.Remove (item);
            }

            return items;
        }
    }
}