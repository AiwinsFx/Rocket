using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// Or规约
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public class OrSpecification<T> : CompositeSpecification<T> {
        /// <summary>
        /// 初始化一个新的规约对象 <see cref="OrSpecification{T}"/> 实例
        /// </summary>
        /// <param name="left">左规约</param>
        /// <param name="right">右规约</param>
        public OrSpecification (ISpecification<T> left, ISpecification<T> right) : base (left, right) { }

        /// <summary>
        /// 获取当前规约的LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        public override Expression<Func<T, bool>> ToExpression () {
            return Left.ToExpression ().Or (Right.ToExpression ());
        }
    }
}