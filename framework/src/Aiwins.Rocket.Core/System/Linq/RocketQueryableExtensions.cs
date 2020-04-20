using System.Linq.Expressions;
using Aiwins.Rocket;
using JetBrains.Annotations;

namespace System.Linq {
    /// <summary>
    /// IQueryable <see cref="IQueryable{T}"/> 相关的扩展方法
    /// </summary>
    public static class RocketQueryableExtensions {
        /// <summary>
        /// IQueryable分页查询
        /// </summary>
        public static IQueryable<T> PageBy<T> ([NotNull] this IQueryable<T> query, int skipCount, int maxResultCount) {
            Check.NotNull (query, nameof (query));

            return query.Skip (skipCount).Take (maxResultCount);
        }

        /// <summary>
        /// IQueryable分页查询
        /// </summary>
        public static TQueryable PageBy<T, TQueryable> ([NotNull] this TQueryable query, int skipCount, int maxResultCount)
        where TQueryable : IQueryable<T> {
            Check.NotNull (query, nameof (query));

            return (TQueryable) query.Skip (skipCount).Take (maxResultCount);
        }
        
        /// <summary>
        /// 当条件成立的时候根据指定表达式过滤 <see cref="IQueryable{T}"/> 。
        /// </summary>
        /// <param name="source">IEnumerable集合</param>
        /// <param name="condition">条件是否满足</param>
        /// <param name="predicate">集合过滤表达式</param>
        /// <returns>根据条件 <paramref name="condition"/> 返回对应的查询表达式 <see cref="IQueryable{T}"/></returns>
        public static IQueryable<T> WhereIf<T> ([NotNull] this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate) {
            Check.NotNull (query, nameof (query));

            return condition ?
                query.Where (predicate) :
                query;
        }

        /// <summary>
        /// 当条件成立的时候根据指定表达式过滤 <see cref="IQueryable{T}"/> 。
        /// </summary>
        /// <param name="source">IEnumerable集合</param>
        /// <param name="condition">条件是否满足</param>
        /// <param name="predicate">集合过滤表达式</param>
        /// <returns>根据条件 <paramref name="condition"/> 返回对应的查询表达式 <see cref="IQueryable{T}"/></returns>
        public static TQueryable WhereIf<T, TQueryable> ([NotNull] this TQueryable query, bool condition, Expression<Func<T, bool>> predicate)
        where TQueryable : IQueryable<T> {
            Check.NotNull (query, nameof (query));

            return condition ?
                (TQueryable) query.Where (predicate) :
                query;
        }

        /// <summary>
        /// 当条件成立的时候根据指定表达式过滤 <see cref="IQueryable{T}"/> 。
        /// </summary>
        /// <param name="source">IEnumerable集合</param>
        /// <param name="condition">条件是否满足</param>
        /// <param name="predicate">集合过滤表达式</param>
        /// <returns>根据条件 <paramref name="condition"/> 返回对应的查询表达式 <see cref="IQueryable{T}"/></returns>
        public static IQueryable<T> WhereIf<T> ([NotNull] this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate) {
            Check.NotNull (query, nameof (query));

            return condition ?
                query.Where (predicate) :
                query;
        }

        /// <summary>
        /// 当条件成立的时候根据指定表达式过滤 <see cref="IQueryable{T}"/> 。
        /// </summary>
        /// <param name="source">IEnumerable集合</param>
        /// <param name="condition">条件是否满足</param>
        /// <param name="predicate">集合过滤表达式</param>
        /// <returns>根据条件 <paramref name="condition"/> 返回对应的查询表达式 <see cref="IQueryable{T}"/></returns>
        public static TQueryable WhereIf<T, TQueryable> ([NotNull] this TQueryable query, bool condition, Expression<Func<T, int, bool>> predicate)
        where TQueryable : IQueryable<T> {
            Check.NotNull (query, nameof (query));

            return condition ?
                (TQueryable) query.Where (predicate) :
                query;
        }
    }
}