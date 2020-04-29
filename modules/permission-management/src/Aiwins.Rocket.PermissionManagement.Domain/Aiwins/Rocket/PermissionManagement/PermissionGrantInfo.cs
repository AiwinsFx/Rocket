using Aiwins.Rocket.Authorization.Permissions;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionGrantInfo {
        public static PermissionGrantInfo NonGranted { get; } = new PermissionGrantInfo (false, nameof (PermissionScopeType.Prohibited));

        public virtual bool IsGranted { get; }

        public virtual string ProviderScope { get; }

        public virtual string ProviderKey { get; }

        public PermissionGrantInfo (bool isGranted, [NotNull] string provideScope, [CanBeNull] string providerKey = null) {
            IsGranted = isGranted;
            ProviderScope = provideScope;
            ProviderKey = providerKey;
        }
    }
}