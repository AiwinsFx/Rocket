﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionDataSeedContributor : IDataSeedContributor, ITransientDependency {
        protected ICurrentTenant CurrentTenant { get; }

        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
        protected IPermissionDataSeeder PermissionDataSeeder { get; }

        public PermissionDataSeedContributor (
            IPermissionDefinitionManager permissionDefinitionManager,
            IPermissionDataSeeder permissionDataSeeder,
            ICurrentTenant currentTenant) {
            PermissionDefinitionManager = permissionDefinitionManager;
            PermissionDataSeeder = permissionDataSeeder;
            CurrentTenant = currentTenant;
        }

        public virtual Task SeedAsync (DataSeedContext context) {
            var multiTenancySide = CurrentTenant.GetMultiTenancySide ();
            var permissionDefinitions = PermissionDefinitionManager
                .GetPermissions ()
                .Where (p => p.MultiTenancySide.HasFlag (multiTenancySide))
                .ToArray ();

            var permissions = new Dictionary<string, string> ();
            foreach (var permissionDefinition in permissionDefinitions) {
                var selectedScope = permissionDefinition.Scopes.FirstOrDefault ();
                // 系统权限需要首先设置Scope范围
                if (selectedScope == null) {
                    throw new RocketException ($"No scopes defined for the permission '{permissionDefinition.Name}', please define permission scopes first.");
                }

                // 系统权限需要设置最大权限作为第一个权限
                if (selectedScope.Name == nameof (PermissionScopeType.Prohibited)) {
                    throw new RocketException ($"Prohibited scope for the permission '{permissionDefinition.Name}' can not set as first scope, please set max scope as the first scope.");
                }

                permissions.Add (permissionDefinition.Name, selectedScope.Name);
            }

            return PermissionDataSeeder.SeedAsync (
                RolePermissionValueProvider.ProviderName,
                "超级管理员",
                permissions,
                context.TenantId
            );
        }
    }
}