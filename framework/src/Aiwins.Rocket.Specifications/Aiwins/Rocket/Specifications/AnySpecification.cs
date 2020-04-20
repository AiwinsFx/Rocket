using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 组合表达式
    /// 任何一个表达式被给定的对象满足
    /// </summary>
    /// <typeparam name="T">表达式类型</typeparam>
    public sealed class AnySpecification<T> : Specification<T> {
        /// <summary>
        /// 获取LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        public override Expression<Func<T, bool>> ToExpression () {
            return o => true;
        }
    }
}