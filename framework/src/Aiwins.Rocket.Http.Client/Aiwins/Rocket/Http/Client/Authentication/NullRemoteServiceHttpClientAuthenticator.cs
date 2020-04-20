using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Http.Client.Authentication {
    [Dependency (TryRegister = true)]
    public class NullRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ISingletonDependency {
        public Task AuthenticateAsync (RemoteServiceHttpClientAuthenticateContext context) {
            return Task.CompletedTask;
        }
    }
}