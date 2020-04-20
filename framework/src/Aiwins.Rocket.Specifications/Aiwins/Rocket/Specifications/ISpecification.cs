using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 规约接口
    /// 关于规范模式的更多信息,可参见
    /// http://martinfowler.com/apsupp/spec.pdf.
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public interface ISpecification<T> {
        /// <summary>
        /// 返回一个 <see cref="bool"/> 值，表明给定的对象是否满足规约
        /// </summary>
        /// <param name="obj">规约对象</param>
        /// <returns>如果满足规约条件，返回true，否则false</returns>
        bool IsSatisfiedBy (T obj);

        /// <summary>
        /// 获取当前规约的LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        Expression<Func<T, bool>> ToExpression ();
    }
}