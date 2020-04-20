namespace Aiwins.Rocket.ObjectMapping {
    /// <summary>
    /// 实体属性映射
    /// </summary>
    public interface IObjectMapper {
        /// <summary>
        /// 对象属性自动映射提供程序 <see cref="IAutoObjectMappingProvider"/> 。
        /// </summary>
        IAutoObjectMappingProvider AutoObjectMappingProvider { get; }

        /// <summary>
        /// 源对象属性映射至目标对象（新创建） <see cref="TDestination"/>.
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TDestination">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        TDestination Map<TSource, TDestination> (TSource source);

        /// <summary>
        /// 源对象属性映射至目标对象（已存在）
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TDestination">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns>返回映射后的目标对象 <see cref="destination"/></returns>
        TDestination Map<TSource, TDestination> (TSource source, TDestination destination);
    }

    /// <summary>
    /// 实体属性映射
    /// </summary>
    public interface IObjectMapper<TContext> : IObjectMapper {

    }

    /// <summary>
    /// 对象实体映射
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public interface IObjectMapper<in TSource, TDestination> {
        /// <summary>
        /// 源对象属性映射至目标对象（新创建） <see cref="TDestination"/>.
        /// </summary>
        /// <param name="source">源对象</param>
        TDestination Map (TSource source);

        /// <summary>
        /// 源对象属性映射至目标对象（已存在）
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns>返回映射后的目标对象 <see cref="destination"/></returns>
        TDestination Map (TSource source, TDestination destination);
    }
}