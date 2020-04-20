using Aiwins.Rocket.Data;
using JetBrains.Annotations;

namespace Aiwins.Rocket.ObjectExtending {
    public static class HasExtraPropertiesObjectExtendingExtensions {
        /// <summary>
        /// 从源对象 <paramref name="source"/> 复制额外属性
        /// 至目标对象 <paramref name="destination"/> 。
        /// 
        /// 基于方法 <paramref name="definitionChecks"/> ， 具体逻辑参见 <see cref="ObjectExtensionManager"/>
        /// </summary>
        /// <param name="TSource">源对象类型</param>
        /// <param name="TDestination">目标对象类型</param>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <param name="definitionChecks">
        /// 用于处理对那些对象进行映射
        /// </param>
        /// <param name="ignoredProperties">可忽略的属性</param>
        public static void MapExtraPropertiesTo<TSource, TDestination> (
            [NotNull] this TSource source, [NotNull] TDestination destination,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
        where TSource : IHasExtraProperties
        where TDestination : IHasExtraProperties {
            ExtensibleObjectMapper.MapExtraPropertiesTo (
                source,
                destination,
                definitionChecks,
                ignoredProperties
            );
        }
    }
}