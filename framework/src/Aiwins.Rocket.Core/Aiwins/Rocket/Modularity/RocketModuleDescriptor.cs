using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity {
    public class RocketModuleDescriptor : IRocketModuleDescriptor {
        public Type Type { get; }

        public Assembly Assembly { get; }

        public IRocketModule Instance { get; }

        public bool IsLoadedAsPlugIn { get; }

        public IReadOnlyList<IRocketModuleDescriptor> Dependencies => _dependencies.ToImmutableList ();
        private readonly List<IRocketModuleDescriptor> _dependencies;

        public RocketModuleDescriptor (
            [NotNull] Type type, [NotNull] IRocketModule instance,
            bool isLoadedAsPlugIn) {
            Check.NotNull (type, nameof (type));
            Check.NotNull (instance, nameof (instance));

            if (!type.GetTypeInfo ().IsAssignableFrom (instance.GetType ())) {
                throw new ArgumentException ($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
            }

            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;

            _dependencies = new List<IRocketModuleDescriptor> ();
        }

        public void AddDependency (IRocketModuleDescriptor descriptor) {
            _dependencies.AddIfNotContains (descriptor);
        }

        public override string ToString () {
            return $"[RocketModuleDescriptor {Type.FullName}]";
        }
    }
}