using System;

namespace Aiwins.Rocket.Features {
    /// <summary>
    /// 此属性可用于类/方法，以声明给定的类/方法可用
    /// 仅当功能启用的时候
    /// </summary>
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiresFeatureAttribute : Attribute {
        /// <summary>
        /// 功能集
        /// </summary>
        public string[] Features { get; }

        /// <summary>
        /// 如果设置为true，则功能集<see cref="Features"/> 所有功能都必须启用
        /// 如果为false, 则功能集 <see cref="Features"/> 中至少保证有一个功能启用
        /// 默认值: false.
        /// </summary>
        public bool RequiresAll { get; set; }

        /// <summary>
        /// 创建一个新的 <see cref="RequiresFeatureAttribute"/> 对象
        /// </summary>
        /// <param name="features">功能集</param>
        public RequiresFeatureAttribute (params string[] features) {
            Features = features ?? Array.Empty<string> ();
        }
    }
}