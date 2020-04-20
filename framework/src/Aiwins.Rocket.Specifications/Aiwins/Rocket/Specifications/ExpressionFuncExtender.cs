using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// Expression表达式
    /// 用于解决EF多条件查询的问题
    /// 更多信息可参见 http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx.
    /// </summary>
    public static class ExpressionFuncExtender {
        private static Expression<T> Compose<T> (this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge) {
            // 参数映射（从第二个参数到第一个参数）
            var map = first.Parameters.Select ((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary (p => p.s, p => p.f);

            // 用第一个lambda表达式中的参数替换第二个lambda表达式中的参数
            var secondBody = ParameterRebinder.ReplaceParameters (map, second.Body);

            // 将lambda表达式体的组合应用于第一个表达式中的参数
            return Expression.Lambda<T> (merge (first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// 通过And语法连接两个表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="first">第一个表达式</param>
        /// <param name="second">第二个表达式</param>
        /// <returns>联合后的表达式</returns>
        public static Expression<Func<T, bool>> And<T> (this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second) {
            return first.Compose (second, Expression.AndAlso);
        }

        /// <summary>
        /// 通过Or语法连接两个表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="first">第一个表达式</param>
        /// <param name="second">第二个表达式</param>
        /// <returns>联合后的表达式</returns>
        public static Expression<Func<T, bool>> Or<T> (this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second) {
            return first.Compose (second, Expression.OrElse);
        }
    }
}