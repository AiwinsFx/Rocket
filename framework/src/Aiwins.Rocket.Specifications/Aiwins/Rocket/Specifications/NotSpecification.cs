using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// Not规约
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public class NotSpecification<T> : Specification<T> {
        private readonly ISpecification<T> _specification;

        /// <summary>
        /// 初始化一个新的规约对象 <see cref="NotSpecification{T}"/> 实例
        /// </summary>
        /// <param name="specification">规约</param>
        public NotSpecification (ISpecification<T> specification) {
            _specification = specification;
        }

        /// <summary>
        /// 获取当前规约的LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        public override Expression<Func<T, bool>> ToExpression () {
            var expression = _specification.ToExpression ();
            return Expression.Lambda<Func<T, bool>> (
                Expression.Not (expression.Body),
                expression.Parameters
            );
        }
    }
}