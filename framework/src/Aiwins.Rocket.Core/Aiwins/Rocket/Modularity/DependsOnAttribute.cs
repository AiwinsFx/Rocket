using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity {
    /// <summary>
    /// 定义类型的依赖项
    /// </summary>
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedTypesProvider {
        [NotNull]
        public Type[] DependedTypes { get; }

        public DependsOnAttribute (params Type[] dependedTypes) {
            DependedTypes = dependedTypes ?? new Type[0];
        }

        public virtual Type[] GetDependedTypes () {
            return DependedTypes;
        }
    }
}