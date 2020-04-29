using System.Collections.Generic;
using Aiwins.Rocket.Authorization.Permissions;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionWithGrantedProviders {
        public string Name { get; }
        public bool IsGranted { get; set; }
        public string Scope { get; set; }

        public List<PermissionValueProviderInfo> Providers { get; set; }

        public PermissionWithGrantedProviders ([NotNull] string name, bool isGranted, [NotNull] string scope = nameof (PermissionScopeType.Prohibited)) {
            Check.NotNull (name, nameof (name));

            Name = name;
            IsGranted = isGranted;
            Scope = scope;

            Providers = new List<PermissionValueProviderInfo> ();
        }
    }
}