using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Aiwins.Rocket.Cli.Http;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Http;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Analyticses
{
    public class CliAnalyticsCollect : ICliAnalyticsCollect, ITransientDependency
    {
        private readonly ICancellationTokenProvider _cancellationTokenProvider;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILogger<CliAnalyticsCollect> _logger;
        private readonly IRemoteServiceExceptionHandler _remoteServiceExceptionHandler;

        public CliAnalyticsCollect(
            ICancellationTokenProvider cancellationTokenProvider,
            IJsonSerializer jsonSerializer,
            IRemoteServiceExceptionHandler remoteServiceExceptionHandler)
        {
            _cancellationTokenProvider = cancellationTokenProvider;
            _jsonSerializer = jsonSerializer;
            _remoteServiceExceptionHandler = remoteServiceExceptionHandler;
            _logger = NullLogger<CliAnalyticsCollect>.Instance;
        }

        public async Task CollectAsync(CliAnalyticsCollectInputDto input)
        {
            var postData = _jsonSerializer.Serialize(input);
            var url = $"{CliUrls.WwwRocketIo}api/clianalytics/collect";
            
            try
            {
                using (var client = new CliHttpClient())
                {
                    var responseMessage = await client.PostAsync(
                        url,
                        new StringContent(postData, Encoding.UTF8, MimeTypes.Application.Json),
                        _cancellationTokenProvider.Token
                    );

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        var exceptionMessage = "Remote server returns '" + (int)responseMessage.StatusCode + "-" + responseMessage.ReasonPhrase + "'. ";
                        var remoteServiceErrorMessage = await _remoteServiceExceptionHandler.GetRocketRemoteServiceErrorAsync(responseMessage);

                        if (remoteServiceErrorMessage != null)
                        {
                            exceptionMessage += remoteServiceErrorMessage;
                        }

                        _logger.LogInformation(exceptionMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}