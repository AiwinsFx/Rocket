using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class PermissionGrantResult {
        public PermissionGrantType GrantType { get; set; }
        public PermissionScopeType ScopeType { get; set; }
        public static PermissionGrantResult Undefined = new PermissionGrantResult { GrantType = PermissionGrantType.Undefined, ScopeType = PermissionScopeType.Prohibited };
        public static PermissionGrantResult Prohibited = new PermissionGrantResult { GrantType = PermissionGrantType.Prohibited, ScopeType = PermissionScopeType.Prohibited };
        public static PermissionGrantResult Granted = new PermissionGrantResult { GrantType = PermissionGrantType.Granted, ScopeType = PermissionScopeType.Prohibited };

        public PermissionGrantResult (bool isGrant, [NotNull] string scope) {
            GrantType = isGrant ? PermissionGrantType.Granted : PermissionGrantType.Prohibited;
            if (Enum.TryParse (scope, out PermissionScopeType scopeType)) {
                ScopeType = scopeType;
            } else {
                ScopeType = PermissionScopeType.Prohibited;
            }
        }

        protected PermissionGrantResult () { }
    }

    public enum PermissionGrantType {
        Undefined,
        Granted,
        Prohibited
    }
}