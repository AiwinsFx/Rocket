using System;

namespace Aiwins.Rocket.Auditing {
    [Serializable]
    public class EntityPropertyChangeInfo {
        /// <summary>
        /// 属性名 <see cref="PropertyName"/> 最大长度
        /// 建议值: 96.
        /// </summary>
        public const int MaxPropertyNameLength = 96;

        /// <summary>
        /// 属性值 <see cref="NewValue"/> 最大长度
        /// 建议值: 512.
        /// </summary>
        public const int MaxValueLength = 512;

        /// <summary>
        /// 属性类型名称 <see cref="PropertyTypeFullName"/> 最大长度
        /// 建议值: 512.
        /// </summary>
        public const int MaxPropertyTypeFullNameLength = 192;

        public virtual string NewValue { get; set; }

        public virtual string OriginalValue { get; set; }

        public virtual string PropertyName { get; set; }

        public virtual string PropertyTypeFullName { get; set; }
    }
}