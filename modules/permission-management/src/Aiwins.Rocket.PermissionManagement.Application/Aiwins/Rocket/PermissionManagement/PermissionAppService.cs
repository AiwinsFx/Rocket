using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.MultiTenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.PermissionManagement {
    [Authorize]
    public class PermissionAppService : ApplicationService, IPermissionAppService {
        protected PermissionManagementOptions Options { get; }
        protected IPermissionManager PermissionManager { get; }
        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }

        public PermissionAppService (
            IPermissionManager permissionManager,
            IPermissionDefinitionManager permissionDefinitionManager,
            IOptions<PermissionManagementOptions> options) {
            Options = options.Value;
            PermissionManager = permissionManager;
            PermissionDefinitionManager = permissionDefinitionManager;
        }

        public virtual async Task<GetPermissionListResultDto> GetAsync (string providerName, string providerKey) {
            await CheckProviderPolicy (providerName);

            var result = new GetPermissionListResultDto {
                EntityDisplayName = providerKey,
                Groups = new List<PermissionGroupDto> ()
            };

            var multiTenancySide = CurrentTenant.GetMultiTenancySide ();

            foreach (var group in PermissionDefinitionManager.GetGroups ()) {
                var groupDto = new PermissionGroupDto {
                    Name = group.Name,
                    DisplayName = group.DisplayName.Localize (StringLocalizerFactory),
                    Permissions = new List<PermissionGrantInfoDto> ()
                };

                foreach (var permission in group.GetPermissionsWithChildren ()) {
                    if (permission.Providers.Any () && !permission.Providers.Contains (providerName)) {
                        continue;
                    }

                    if (!permission.MultiTenancySide.HasFlag (multiTenancySide)) {
                        continue;
                    }

                    var grantInfoDto = new PermissionGrantInfoDto {
                        Name = permission.Name,
                        DisplayName = permission.DisplayName.Localize (StringLocalizerFactory),
                        ParentName = permission.Parent?.Name,
                        // 仅当scopes的元素为两个，且仅包含禁止和启用两种权限的时候才可以使用非下拉框的类型控件
                        IsDropdownBox = permission.Scopes.Count == 2 && permission.Scopes.Contains(PermissionDefinitionProvider.Prohibited)  && permission.Scopes.Contains(PermissionDefinitionProvider.Granted) ? permission.IsDropdownBox : true,
                        AllowedProviders = permission.Providers,
                        Scopes = permission.Scopes.Where (m => !m.MultiTenancySide.HasFlag (multiTenancySide)).Select (m => new PermissionScopeDto () { Name = m.Name, DisplayName = m.DisplayName }).ToList (),
                        GrantedProviders = new List<ProviderInfoDto> ()
                    };

                    var grantInfo = await PermissionManager.GetAsync (permission.Name, providerName, providerKey);

                    grantInfoDto.IsGranted = grantInfo.IsGranted;
                    grantInfoDto.SelectedScope = grantInfo.Scope;

                    foreach (var provider in grantInfo.Providers) {
                        grantInfoDto.GrantedProviders.Add (new ProviderInfoDto {
                            ProviderName = provider.Name,
                                ProviderScope = provider.Scope,
                                ProviderKey = provider.Key,
                        });
                    }

                    groupDto.Permissions.Add (grantInfoDto);
                }

                if (groupDto.Permissions.Any ()) {
                    result.Groups.Add (groupDto);
                }
            }

            return result;
        }

        public virtual async Task UpdateAsync (string providerName, string providerKey, UpdatePermissionsDto input) {
            await CheckProviderPolicy (providerName);

            foreach (var permissionDto in input.Permissions) {
                await PermissionManager.SetAsync (permissionDto.Name, providerName, providerKey, permissionDto.Scope);
            }
        }

        protected virtual async Task CheckProviderPolicy (string providerName) {
            var policyName = Options.ProviderPolicies.GetOrDefault (providerName);
            if (policyName.IsNullOrEmpty ()) {
                throw new RocketException ($"No policy defined to get/set permissions for the provider '{policyName}'. Use {nameof(PermissionManagementOptions)} to map the policy.");
            }

            await AuthorizationService.CheckAsync (policyName);
        }
    }
}