using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// And规约
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public class AndSpecification<T> : CompositeSpecification<T> {
        /// <summary>
        /// 构造函数 <see cref="AndSpecification{T}"/>
        /// </summary>
        /// <param name="left">左规约</param>
        /// <param name="right">右规约</param>
        public AndSpecification (ISpecification<T> left, ISpecification<T> right) : base (left, right) { }

        /// <summary>
        /// 获取LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        public override Expression<Func<T, bool>> ToExpression () {
            return Left.ToExpression ().And (Right.ToExpression ());
        }
    }
}