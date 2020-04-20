using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// AndNot规约
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public class AndNotSpecification<T> : CompositeSpecification<T> {
        /// <summary>
        /// 构造函数 <see cref="AndNotSpecification{T}"/>
        /// </summary>
        /// <param name="left">左规约</param>
        /// <param name="right">右规约</param>
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