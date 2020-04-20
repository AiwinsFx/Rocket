using System;

namespace Aiwins.Rocket.ObjectExtending {
    [Flags]
    public enum MappingPropertyDefinitionChecks : byte {
        /// <summary>
        /// 不进行属性检查。将源对象的额外属性信息复制到目标对象
        /// </summary>
        None = 0,

        /// <summary>
        /// 复制源对象的额外属性信息
        /// </summary>
        Source = 1,

        /// <summary>
        /// 复制目标对象的额外属性信息
        /// </summary>
        Destination = 2,

        /// <summary>
        /// 二者皆复制
        /// </summary>
        Both = Source | Destination
    }
}