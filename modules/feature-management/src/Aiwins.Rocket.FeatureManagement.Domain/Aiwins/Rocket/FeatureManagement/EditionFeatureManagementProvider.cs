using System.Security.Principal;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Features;
using Aiwins.Rocket.Security.Claims;

namespace Aiwins.Rocket.FeatureManagement {
    public class EditionFeatureManagementProvider : FeatureManagementProvider, ITransientDependency {
        public override string Name => EditionFeatureValueProvider.ProviderName;

        protected ICurrentPrincipalAccessor PrincipalAccessor { get; }

        public EditionFeatureManagementProvider (
            IFeatureManagementStore store,
            ICurrentPrincipalAccessor principalAccessor) : base (store) {
            PrincipalAccessor = principalAccessor;
        }

        protected override string NormalizeProviderKey (string providerKey) {
            if (providerKey != null) {
                return providerKey;
            }

            return PrincipalAccessor.Principal?.FindEditionId ()?.ToString ();
        }
    }
}