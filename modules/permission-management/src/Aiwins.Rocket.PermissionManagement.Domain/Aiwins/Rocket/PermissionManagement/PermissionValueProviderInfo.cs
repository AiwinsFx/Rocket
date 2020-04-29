using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionValueProviderInfo {
        public string Name { get; }

        public string Scope { get; }

        public string Key { get; }

        public PermissionValueProviderInfo ([NotNull] string name, [NotNull] string scope, [NotNull] string key) {
            Check.NotNull (name, nameof (name));
            Check.NotNull (scope, nameof (scope));
            Check.NotNull (key, nameof (key));

            Name = name;
            Scope = scope;
            Key = key;
        }
    }
}