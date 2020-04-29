using System.Collections.Generic;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionManagementOptions {
        public Dictionary<string, string> ProviderPolicies { get; }

        public PermissionManagementOptions () {
            ProviderPolicies = new Dictionary<string, string> ();
        }
    }
}