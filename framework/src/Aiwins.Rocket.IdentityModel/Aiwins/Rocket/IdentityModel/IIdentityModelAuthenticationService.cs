using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.IdentityModel {
    //TODO: 重新考虑这个接口
    public interface IIdentityModelAuthenticationService {
        Task<bool> TryAuthenticateAsync (
            [NotNull] HttpClient client,
            string identityClientName = null);

        Task<string> GetAccessTokenAsync (
            IdentityClientConfiguration configuration);
    }
}