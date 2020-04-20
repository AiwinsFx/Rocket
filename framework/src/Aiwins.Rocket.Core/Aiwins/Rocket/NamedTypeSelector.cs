using System;

namespace Aiwins.Rocket {
    public class NamedTypeSelector {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 条件表达式
        /// </summary>
        public Func<Type, bool> Predicate { get; set; }

        /// <summary>
        /// 创建NamedTypeSelector <see cref="NamedTypeSelector"/> 对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="predicate">条件表达式</param>
        public NamedTypeSelector (string name, Func<Type, bool> predicate) {
            Name = name;
            Predicate = predicate;
        }
    }
}