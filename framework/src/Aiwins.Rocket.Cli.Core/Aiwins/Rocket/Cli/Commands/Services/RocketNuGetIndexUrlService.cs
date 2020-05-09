using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Aiwins.Rocket.Cli.Licensing;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.Commands.Services
{
    public class RocketNuGetIndexUrlService : ITransientDependency
    {
        private readonly IApiKeyService _apiKeyService;
        public ILogger<RocketNuGetIndexUrlService> Logger { get; set; }

        public RocketNuGetIndexUrlService(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
            Logger = NullLogger<RocketNuGetIndexUrlService>.Instance;
        }

        public async Task<string> GetAsync()
        {
            var apiKeyResult = await _apiKeyService.GetApiKeyOrNullAsync();

            if (apiKeyResult == null)
            {
                Logger.LogWarning("You are not signed in! Use the CLI command \"rocket login <username>\" to sign in, then try again.");
                return null;
            }

            if (!string.IsNullOrWhiteSpace(apiKeyResult.ErrorMessage))
            {
                Logger.LogWarning(apiKeyResult.ErrorMessage);
                return null;
            }

            if (string.IsNullOrEmpty(apiKeyResult.ApiKey))
            {
                Logger.LogError("Couldn't retrieve your NuGet API key! You can re-sign in with the CLI command \"rocket login <username>\".");
                return null;
            }

            return CliUrls.GetNuGetServiceIndexUrl(apiKeyResult.ApiKey);
        }
    }
}
