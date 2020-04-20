using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Authorization {
    public class RocketAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider, IRocketAuthorizationPolicyProvider, ITransientDependency {
        private readonly AuthorizationOptions _options;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public RocketAuthorizationPolicyProvider (
            IOptions<AuthorizationOptions> options,
            IPermissionDefinitionManager permissionDefinitionManager) : base (options) {
            _permissionDefinitionManager = permissionDefinitionManager;
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync (string policyName) {
            var policy = await base.GetPolicyAsync (policyName);
            if (policy != null) {
                return policy;
            }

            var permission = _permissionDefinitionManager.GetOrNull (policyName);
            if (permission != null) {
                //TODO: 考虑通过缓存的方式进行优化!
                var policyBuilder = new AuthorizationPolicyBuilder (Array.Empty<string> ());
                policyBuilder.Requirements.Add (new PermissionRequirement (policyName));
                return policyBuilder.Build ();
            }

            return null;
        }

        public Task<List<string>> GetPoliciesNamesAsync () {
            return Task.FromResult (
                _options.GetPoliciesNames ()
                .Union (
                    _permissionDefinitionManager
                    .GetPermissions ()
                    .Select (p => p.Name)
                )
                .ToList ()
            );
        }
    }
}