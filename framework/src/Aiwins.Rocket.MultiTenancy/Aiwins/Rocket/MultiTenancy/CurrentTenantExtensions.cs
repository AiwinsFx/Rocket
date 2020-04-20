﻿using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.MultiTenancy {
    public static class CurrentTenantExtensions {
        public static Guid GetId ([NotNull] this ICurrentTenant currentTenant) {
            Check.NotNull (currentTenant, nameof (currentTenant));

            if (currentTenant.Id == null) {
                throw new RocketException ("Current Tenant Id is not available!");
            }

            return currentTenant.Id.Value;
        }

        public static MultiTenancySides GetMultiTenancySide (this ICurrentTenant currentTenant) {
            return currentTenant.Id.HasValue ?
                MultiTenancySides.Tenant :
                MultiTenancySides.Host;
        }
    }
}