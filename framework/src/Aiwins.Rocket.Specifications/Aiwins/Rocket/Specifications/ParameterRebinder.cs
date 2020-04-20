using System.Collections.Generic;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 参数绑定
    /// 更多信息可参见 http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx.
    /// </summary>
    internal class ParameterRebinder : ExpressionVisitor {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        internal ParameterRebinder (Dictionary<ParameterExpression, ParameterExpression> map) {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression> ();
        }

        internal static Expression ReplaceParameters (Dictionary<ParameterExpression, ParameterExpression> map,
            Expression exp) {
            return new ParameterRebinder (map).Visit (exp);
        }

        protected override Expression VisitParameter (ParameterExpression p) {
            if (_map.TryGetValue (p, out var replacement)) {
                p = replacement;
            }

            return base.VisitParameter (p);
        }
    }
}