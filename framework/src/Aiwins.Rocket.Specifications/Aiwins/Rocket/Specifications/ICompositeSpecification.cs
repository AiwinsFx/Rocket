namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 组合规约
    /// </summary>
    /// <typeparam name="T">规约类型</typeparam>
    public interface ICompositeSpecification<T> : ISpecification<T> {
        /// <summary>
        /// 左规约
        /// </summary>
        ISpecification<T> Left { get; }

        /// <summary>
        /// 右规约
        /// </summary>
        ISpecification<T> Right { get; }
    }
}