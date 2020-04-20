using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 组合表达式
    /// 两个表达式需要被给定的对象满足
    /// </summary>
    /// <typeparam name="T">表达式类型</typeparam>
    public class AndSpecification<T> : CompositeSpecification<T> {
        /// <summary>
        /// 构造函数 <see cref="AndSpecification{T}"/>
        /// </summary>
        /// <param name="left">第一个表达式</param>
        /// <param name="right">第二个表达式</param>
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