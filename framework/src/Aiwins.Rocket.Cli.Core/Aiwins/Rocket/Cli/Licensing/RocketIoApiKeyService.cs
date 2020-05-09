using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Polly;
using Polly.Extensions.Http;
using Aiwins.Rocket.Cli.Auth;
using Aiwins.Rocket.Cli.Http;
using Aiwins.Rocket.Cli.ProjectBuilding;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.Cli.Licensing
{
    public class RocketIoApiKeyService : IApiKeyService, ITransientDependency
    {
        protected IJsonSerializer JsonSerializer { get; }
        protected IRemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }
        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        private readonly ILogger<RocketIoApiKeyService> _logger;
        private DeveloperApiKeyResult _apiKeyResult = null;

        public RocketIoApiKeyService(
            IJsonSerializer jsonSerializer,
            ICancellationTokenProvider cancellationTokenProvider,
            IRemoteServiceExceptionHandler remoteServiceExceptionHandler,
            ILogger<RocketIoApiKeyService> logger)
        {
            JsonSerializer = jsonSerializer;
            RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
            _logger = logger;
            CancellationTokenProvider = cancellationTokenProvider;
        }

        public async Task<DeveloperApiKeyResult> GetApiKeyOrNullAsync(bool invalidateCache = false)
        {
            if (!AuthService.IsLoggedIn())
            {
                return null;
            }

            if (invalidateCache)
            {
                _apiKeyResult = null;
            }

            if (_apiKeyResult != null)
            {
                return _apiKeyResult;
            }

            var url = $"{CliUrls.WwwRocketIo}api/license/api-key";

            using (var client = new CliHttpClient())
            {
                var response = await client.GetHttpResponseMessageWithRetryAsync(
                    url: url,
                    cancellationToken: CancellationTokenProvider.Token,
                    logger: _logger);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"ERROR: Remote server returns '{response.StatusCode}'");
                }

                await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync(response);

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiKeyResult = JsonSerializer.Deserialize<DeveloperApiKeyResult>(responseContent);

                return apiKeyResult;
            }
        }
    }
}