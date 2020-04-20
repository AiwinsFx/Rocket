using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// Any规约
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
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