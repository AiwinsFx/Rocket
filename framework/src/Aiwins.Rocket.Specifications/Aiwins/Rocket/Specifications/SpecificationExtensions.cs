using JetBrains.Annotations;

namespace Aiwins.Rocket.Specifications {
    public static class SpecificationExtensions {
        /// <summary>
        /// 将当前规约与另一个规约组合
        /// 并返回同时满足当前规约和另一个规约的组合规约
        /// </summary>
        /// <param name="specification">给定规约</param>
        /// <param name="other">其他规约</param>
        /// <returns>And规约实例</returns>
        public static ISpecification<T> And<T> ([NotNull] this ISpecification<T> specification, [NotNull] ISpecification<T> other) {
            Check.NotNull (specification, nameof (specification));
            Check.NotNull (other, nameof (other));

            return new AndSpecification<T> (specification, other);
        }

        /// <summary>
        /// 将当前规约与另一个规约组合
        /// 并返回满足当前规约或者满足另一个规约的组合规约
        /// </summary>
        /// <param name="specification">给定规约</param>
        /// <param name="other">其他规约</param>
        /// <returns>Or规约实例</returns>
        public static ISpecification<T> Or<T> ([NotNull] this ISpecification<T> specification, [NotNull] ISpecification<T> other) {
            Check.NotNull (specification, nameof (specification));
            Check.NotNull (other, nameof (other));

            return new OrSpecification<T> (specification, other);
        }

        /// <summary>
        /// 将当前规约与另一个规约组合
        /// 并返回满足当前规约而不满足另一个规约的组合规约
        /// </summary>
        /// <param name="specification">给定规约</param>
        /// <param name="other">其他规约</param>
        /// <returns>AndNot组合规约实例</returns>
        public static ISpecification<T> AndNot<T> ([NotNull] this ISpecification<T> specification, [NotNull] ISpecification<T> other) {
            Check.NotNull (specification, nameof (specification));
            Check.NotNull (other, nameof (other));

            return new AndNotSpecification<T> (specification, other);
        }

        /// <summary>
        /// 反转当前规约语义
        /// </summary>
        /// <returns>Not规约实例（和给定的规约语义相反）</returns>
        public static ISpecification<T> Not<T> ([NotNull] this ISpecification<T> specification) {
            Check.NotNull (specification, nameof (specification));

            return new NotSpecification<T> (specification);
        }
    }
}