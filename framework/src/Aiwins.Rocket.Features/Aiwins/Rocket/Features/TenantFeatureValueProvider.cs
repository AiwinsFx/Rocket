﻿using System.Threading.Tasks;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.Features {
    public class TenantFeatureValueProvider : FeatureValueProvider {
        public const string ProviderName = "T";

        public override string Name => ProviderName;

        protected ICurrentTenant CurrentTenant { get; }

        public TenantFeatureValueProvider (IFeatureStore featureStore, ICurrentTenant currentTenant) : base (featureStore) {
            CurrentTenant = currentTenant;
        }

        public override async Task<string> GetOrNullAsync (FeatureDefinition feature) {
            return await FeatureStore.GetOrNullAsync (feature.Name, Name, CurrentTenant.Id?.ToString ());
        }
    }
}