using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class RocketPermissionOptions {
        public ITypeList<IPermissionDefinitionProvider> DefinitionProviders { get; }

        public ITypeList<IPermissionValueProvider> ValueProviders { get; }

        public PermissionPolicy PermissionPolicy { get; set; }

        public RocketPermissionOptions () {
            DefinitionProviders = new TypeList<IPermissionDefinitionProvider> ();
            ValueProviders = new TypeList<IPermissionValueProvider> ();
        }
    }
}