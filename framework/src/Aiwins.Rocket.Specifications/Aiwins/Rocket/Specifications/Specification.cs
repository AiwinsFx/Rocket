using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 规约基类
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public abstract class Specification<T> : ISpecification<T> {
        /// <summary>
        /// 返回一个 <see cref="bool"/> 值，表明给定的对象是否满足规约
        /// </summary>
        /// <param name="obj">规约对象</param>
        /// <returns>如果满足规约条件，返回true，否则false</returns>
        public virtual bool IsSatisfiedBy (T obj) {
            return ToExpression ().Compile () (obj);
        }

        /// <summary>
        /// 获取当前规约的LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        public abstract Expression<Func<T, bool>> ToExpression ();

        /// <summary>
        /// 规约转换为表达式
        /// </summary>
        /// <param name="specification"></param>
        public static implicit operator Expression<Func<T, bool>> (Specification<T> specification) {
            return specification.ToExpression ();
        }
    }
}