using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.Data;
using JetBrains.Annotations;

namespace Aiwins.Rocket.ObjectExtending {
    public static class ExtensibleObjectMapper {
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
            [NotNull] TSource source, [NotNull] TDestination destination,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
        where TSource : IHasExtraProperties
        where TDestination : IHasExtraProperties {
            Check.NotNull (source, nameof (source));
            Check.NotNull (destination, nameof (destination));

            ExtensibleObjectMapper.MapExtraPropertiesTo (
                typeof (TSource),
                typeof (TDestination),
                source.ExtraProperties,
                destination.ExtraProperties,
                definitionChecks,
                ignoredProperties
            );
        }

        /// <summary>
        /// 从源对象 <paramref name="sourceDictionary"/> 复制额外属性
        /// 至目标对象 <paramref name="destinationDictionary"/> 。
        /// 
        /// 基于方法 <paramref name="definitionChecks"/> ， 具体逻辑参见 <see cref="ObjectExtensionManager"/>
        /// </summary>
        /// <param name="TSource">源对象类型</param>
        /// <param name="TDestination">目标对象类型</param>
        /// <param name="sourceDictionary">源对象</param>
        /// <param name="destinationDictionary">目标对象</param>
        /// <param name="definitionChecks">
        /// 用于处理对那些对象进行映射
        /// </param>
        /// <param name="ignoredProperties">可忽略的属性</param>
        public static void MapExtraPropertiesTo<TSource, TDestination> (
            [NotNull] Dictionary<string, object> sourceDictionary, [NotNull] Dictionary<string, object> destinationDictionary,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null)
        where TSource : IHasExtraProperties
        where TDestination : IHasExtraProperties {
            MapExtraPropertiesTo (
                typeof (TSource),
                typeof (TDestination),
                sourceDictionary,
                destinationDictionary,
                definitionChecks,
                ignoredProperties
            );
        }

        /// <summary>
        /// 从源对象 <paramref name="sourceDictionary"/> 复制额外属性
        /// 至目标对象 <paramref name="destinationDictionary"/> 。
        /// 
        /// 基于方法 <paramref name="definitionChecks"/> ， 具体逻辑参见 <see cref="ObjectExtensionManager"/>
        /// </summary>
        /// <param name="sourceType">源对象类型</param>
        /// <param name="destinationType">目标对象类型</param>
        /// <param name="sourceDictionary">源对象</param>
        /// <param name="destinationDictionary">目标对象</param>
        /// <param name="definitionChecks">
        /// 用于处理对那些对象进行映射
        /// </param>
        /// <param name="ignoredProperties">可忽略的属性</param>
        public static void MapExtraPropertiesTo (
            [NotNull] Type sourceType, [NotNull] Type destinationType, [NotNull] Dictionary<string, object> sourceDictionary, [NotNull] Dictionary<string, object> destinationDictionary,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null) {
            Check.AssignableTo<IHasExtraProperties> (sourceType, nameof (sourceType));
            Check.AssignableTo<IHasExtraProperties> (destinationType, nameof (destinationType));
            Check.NotNull (sourceDictionary, nameof (sourceDictionary));
            Check.NotNull (destinationDictionary, nameof (destinationDictionary));

            var sourceObjectExtension = ObjectExtensionManager.Instance.GetOrNull (sourceType);
            var destinationObjectExtension = ObjectExtensionManager.Instance.GetOrNull (destinationType);

            foreach (var keyValue in sourceDictionary) {
                if (CanMapProperty (
                        keyValue.Key,
                        sourceObjectExtension,
                        destinationObjectExtension,
                        definitionChecks,
                        ignoredProperties)) {
                    destinationDictionary[keyValue.Key] = keyValue.Value;
                }
            }
        }

        public static bool CanMapProperty<TSource, TDestination> (
            [NotNull] string propertyName,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null) {
            return CanMapProperty (
                typeof (TSource),
                typeof (TDestination),
                propertyName,
                definitionChecks,
                ignoredProperties
            );
        }

        public static bool CanMapProperty (
            [NotNull] Type sourceType, [NotNull] Type destinationType, [NotNull] string propertyName,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null) {
            Check.AssignableTo<IHasExtraProperties> (sourceType, nameof (sourceType));
            Check.AssignableTo<IHasExtraProperties> (destinationType, nameof (destinationType));
            Check.NotNull (propertyName, nameof (propertyName));

            var sourceObjectExtension = ObjectExtensionManager.Instance.GetOrNull (sourceType);
            var destinationObjectExtension = ObjectExtensionManager.Instance.GetOrNull (destinationType);

            return CanMapProperty (
                propertyName,
                sourceObjectExtension,
                destinationObjectExtension,
                definitionChecks,
                ignoredProperties);
        }

        private static bool CanMapProperty (
            [NotNull] string propertyName, [CanBeNull] ObjectExtensionInfo sourceObjectExtension, [CanBeNull] ObjectExtensionInfo destinationObjectExtension,
            MappingPropertyDefinitionChecks? definitionChecks = null,
            string[] ignoredProperties = null) {
            Check.NotNull (propertyName, nameof (propertyName));

            if (ignoredProperties != null &&
                ignoredProperties.Contains (propertyName)) {
                return false;
            }

            if (definitionChecks != null) {
                if (definitionChecks.Value.HasFlag (MappingPropertyDefinitionChecks.Source)) {
                    if (sourceObjectExtension == null) {
                        return false;
                    }

                    if (!sourceObjectExtension.HasProperty (propertyName)) {
                        return false;
                    }
                }

                if (definitionChecks.Value.HasFlag (MappingPropertyDefinitionChecks.Destination)) {
                    if (destinationObjectExtension == null) {
                        return false;
                    }

                    if (!destinationObjectExtension.HasProperty (propertyName)) {
                        return false;
                    }
                }

                return true;
            } else {
                var sourcePropertyDefinition = sourceObjectExtension?.GetPropertyOrNull (propertyName);
                var destinationPropertyDefinition = destinationObjectExtension?.GetPropertyOrNull (propertyName);

                if (sourcePropertyDefinition != null) {
                    if (destinationPropertyDefinition != null) {
                        return true;
                    }

                    if (sourcePropertyDefinition.CheckPairDefinitionOnMapping == false) {
                        return true;
                    }
                } else if (destinationPropertyDefinition != null) {
                    if (destinationPropertyDefinition.CheckPairDefinitionOnMapping == false) {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}