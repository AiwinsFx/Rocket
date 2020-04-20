using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.Modularity {
    public class RocketModuleLifecycleOptions {
        public ITypeList<IModuleLifecycleContributor> Contributors { get; }

        public RocketModuleLifecycleOptions () {
            Contributors = new TypeList<IModuleLifecycleContributor> ();
        }
    }
}