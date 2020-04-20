using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// None规约
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public sealed class NoneSpecification<T> : Specification<T> {
        /// <summary>
        /// 获取当前规约的LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        public override Expression<Func<T, bool>> ToExpression () {
            return o => false;
        }
    }
}