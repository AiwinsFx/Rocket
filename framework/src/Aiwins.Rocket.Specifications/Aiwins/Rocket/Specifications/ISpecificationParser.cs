namespace Aiwins.Rocket.Specifications {
    /// <summary>
    /// 将给定规约解析为特定领域的条件对象，
    /// 比如满足NHibernate库中的 <c>ICriteria</c> 接口对象
    /// </summary>
    /// <typeparam name="TCriteria">特定领域的条件对象类型</typeparam>
    public interface ISpecificationParser<out TCriteria> {
        /// <summary>
        /// 将给定规约解析为特定领域的条件对象。
        /// </summary>
        /// <typeparam name="T">规约类型</typeparam>
        /// <param name="specification">规约实例</param>
        /// <returns>特定领域的条件对象</returns>
        TCriteria Parse<T> (ISpecification<T> specification);
    }
}