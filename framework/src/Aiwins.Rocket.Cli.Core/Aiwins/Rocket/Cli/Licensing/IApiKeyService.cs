using System.Threading.Tasks;

namespace Aiwins.Rocket.Cli.Licensing {
    public interface IApiKeyService {
        Task<DeveloperApiKeyResult> GetApiKeyOrNullAsync (bool invalidateCache = false);
    }
}