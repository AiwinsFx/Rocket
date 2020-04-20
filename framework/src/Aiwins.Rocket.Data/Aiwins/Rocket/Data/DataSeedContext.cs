using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Data {
    public class DataSeedContext {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 设置属性字典 <see cref="Properties"/>.
        /// </summary>
        /// <param name="name">属性的名称</param>
        /// <returns>
        /// 通过指定名称 <see cref="name"/> 返回属性字典 <see cref="Properties"/>  中查询到的属性值
        /// 如果指定名称 <see cref="name"/> 的值未检索到，则返回null。
        /// </returns>
        [CanBeNull]
        public object this [string name] {
            get => Properties.GetOrDefault (name);
            set => Properties[name] = value;
        }

        /// <summary>
        /// 自定义属性
        /// </summary>
        [NotNull]
        public Dictionary<string, object> Properties { get; }

        public DataSeedContext (Guid? tenantId = null) {
            TenantId = tenantId;
            Properties = new Dictionary<string, object> ();
        }

        /// <summary>
        /// 设置属性 <see cref="Properties"/> 的语法糖
        /// </summary>
        public virtual DataSeedContext WithProperty (string key, object value) {
            Properties[key] = value;
            return this;
        }
    }
}