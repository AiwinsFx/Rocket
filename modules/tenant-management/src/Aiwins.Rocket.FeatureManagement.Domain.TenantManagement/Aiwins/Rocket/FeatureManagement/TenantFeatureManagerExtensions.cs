﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Features;
using JetBrains.Annotations;

namespace Aiwins.Rocket.FeatureManagement {
    public static class TenantFeatureManagerExtensions {
        public static Task<string> GetOrNullForTenantAsync (this IFeatureManager featureManager, [NotNull] string name, Guid tenantId, bool fallback = true) {
            return featureManager.GetOrNullAsync (name, TenantFeatureValueProvider.ProviderName, tenantId.ToString (), fallback);
        }

        public static Task<List<FeatureNameValue>> GetAllForTenantAsync (this IFeatureManager featureManager, Guid tenantId, bool fallback = true) {
            return featureManager.GetAllAsync (TenantFeatureValueProvider.ProviderName, tenantId.ToString (), fallback);
        }

        public static Task SetForTenantAsync (this IFeatureManager featureManager, Guid tenantId, [NotNull] string name, [CanBeNull] string value, bool forceToSet = false) {
            return featureManager.SetAsync (name, value, TenantFeatureValueProvider.ProviderName, tenantId.ToString (), forceToSet);
        }
    }
}