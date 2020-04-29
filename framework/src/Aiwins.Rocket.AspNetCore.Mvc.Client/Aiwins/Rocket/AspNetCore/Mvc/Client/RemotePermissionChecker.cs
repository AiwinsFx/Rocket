using System.Security.Claims;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.Client {
    public class RemotePermissionChecker : IPermissionChecker, ITransientDependency {
        protected ICachedApplicationConfigurationClient ConfigurationClient { get; }

        public RemotePermissionChecker (ICachedApplicationConfigurationClient configurationClient) {
            ConfigurationClient = configurationClient;
        }

        public async Task<PermissionGrantResult> GetResultAsync (string name) {
            var configuration = await ConfigurationClient.GetAsync ();

            return configuration.Auth.GrantedPolicies.ContainsKey (name) ? PermissionGrantResult.Granted : PermissionGrantResult.Prohibited;
        }

        public async Task<PermissionGrantResult> GetResultAsync (ClaimsPrincipal claimsPrincipal, string name) {
            /* This provider always works for the current principal. */
            return await GetResultAsync (name);
        }
    }
}