namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 复合规约基类
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public abstract class CompositeSpecification<T> : Specification<T>, ICompositeSpecification<T> {
        /// <summary>
        /// 初始化一个新的复合规约 <see cref="CompositeSpecification{T}"/> 实例
        /// </summary>
        /// <param name="left">The first specification.</param>
        /// <param name="right">The second specification.</param>
        protected CompositeSpecification (ISpecification<T> left, ISpecification<T> right) {
            Left = left;
            Right = right;
        }

        /// <summary>
        /// 左规约
        /// </summary>
        public ISpecification<T> Left { get; }

        /// <summary>
        /// 右规约
        /// </summary>
        public ISpecification<T> Right { get; }
    }
}