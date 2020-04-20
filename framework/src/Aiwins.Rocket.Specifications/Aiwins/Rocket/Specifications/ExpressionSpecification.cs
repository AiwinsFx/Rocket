using System;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// LINQ表达式构建的规约
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public class ExpressionSpecification<T> : Specification<T> {
        private readonly Expression<Func<T, bool>> _expression;

        /// <summary>
        /// 初始化一个新的表达式规约 <c>ExpressionSpecification&lt;T&gt;</c> 实例
        /// </summary>
        /// <param name="expression">当前规约的LINQ表达式</param>
        public ExpressionSpecification (Expression<Func<T, bool>> expression) {
            _expression = expression;
        }

        /// <summary>
        /// 获取当前规约的LINQ表达式
        /// </summary>
        /// <returns>LINQ表达式</returns>
        public override Expression<Func<T, bool>> ToExpression () {
            return _expression;
        }
    }
}