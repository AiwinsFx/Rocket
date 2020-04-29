using System.Collections.Generic;
using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.FeatureManagement {
    public class FeatureManagementOptions {
        public ITypeList<IFeatureManagementProvider> Providers { get; }

        public Dictionary<string, string> ProviderPolicies { get; }

        public FeatureManagementOptions () {
            Providers = new TypeList<IFeatureManagementProvider> ();
            ProviderPolicies = new Dictionary<string, string> ();
        }
    }
}