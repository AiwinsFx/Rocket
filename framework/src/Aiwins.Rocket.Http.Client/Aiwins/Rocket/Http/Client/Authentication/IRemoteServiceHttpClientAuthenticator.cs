using System.Threading.Tasks;

namespace Aiwins.Rocket.Http.Client.Authentication {
    public interface IRemoteServiceHttpClientAuthenticator {
        Task AuthenticateAsync (RemoteServiceHttpClientAuthenticateContext context);
    }
}