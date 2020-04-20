using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 组合表达式
    /// 第一个表达式需要被给定的对象满足，而第二个表达式不能满足。
    /// </summary>
    /// <typeparam name="T">表达式类型</typeparam>
    public class AndNotSpecification<T> : CompositeSpecification<T> {
        /// <summary>
        /// 构造函数 <see cref="AndNotSpecification{T}"/>
        /// </summary>
        /// <param name="left">第一个表达式</param>
        /// <param name="right">第二个表达式</param>
        public AndNotSpecification (ISpecification<T> left, ISpecification<T> right) : base (left, right) { }

        /// <summary>
        /// 获取LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        public override Expression<Func<T, bool>> ToExpression () {
            var rightExpression = Right.ToExpression ();

            var bodyNot = Expression.Not (rightExpression.Body);
            var bodyNotExpression = Expression.Lambda<Func<T, bool>> (bodyNot, rightExpression.Parameters);

            return Left.ToExpression ().And (bodyNotExpression);
        }
    }
}