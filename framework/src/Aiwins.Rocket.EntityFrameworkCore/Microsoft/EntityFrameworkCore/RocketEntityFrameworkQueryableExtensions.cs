using System;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore {
    public static class RocketEntityFrameworkQueryableExtensions {
        /// <summary>
        /// 指定要包含在查询结果中的相关实体
        /// </summary>
        /// <param name="source">查询语句 <see cref="IQueryable{T}"/> </param>
        /// <param name="condition">满足条件则结果中包含指定的实体 <paramref name="path"/></param>
        /// <param name="path">相关实体</param>
        public static IQueryable<T> IncludeIf<T, TProperty> (this IQueryable<T> source, bool condition, Expression<Func<T, TProperty>> path)
        where T : class {
            return condition ?
                source.Include (path) :
                source;
        }
    }
}