using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Http.Client.Authentication;
using Aiwins.Rocket.IdentityModel;

namespace Aiwins.Rocket.Http.Client.IdentityModel {
    [Dependency (ReplaceServices = true)]
    public class IdentityModelRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ITransientDependency {
        protected IIdentityModelAuthenticationService IdentityModelAuthenticationService { get; }

        public IdentityModelRemoteServiceHttpClientAuthenticator (
            IIdentityModelAuthenticationService identityModelAuthenticationService) {
            IdentityModelAuthenticationService = identityModelAuthenticationService;
        }

        public virtual async Task AuthenticateAsync (RemoteServiceHttpClientAuthenticateContext context) {
            await IdentityModelAuthenticationService.TryAuthenticateAsync (
                context.Client,
                context.RemoteService.GetIdentityClient ()
            );
        }
    }
}