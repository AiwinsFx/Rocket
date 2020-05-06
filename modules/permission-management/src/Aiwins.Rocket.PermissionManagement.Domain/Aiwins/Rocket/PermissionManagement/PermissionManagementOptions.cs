using System.Collections.Generic;
using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionManagementOptions {
        public ITypeList<IPermissionManagementProvider> Providers { get; }
        public Dictionary<string, string> ProviderPolicies { get; }

        public PermissionManagementOptions () {
            Providers = new TypeList<IPermissionManagementProvider> ();
            ProviderPolicies = new Dictionary<string, string> ();
        }
    }
}