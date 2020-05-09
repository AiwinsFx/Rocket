using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Aiwins.Rocket.Cli.Http;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.ProjectModification
{
    public class MyGetPackageListFinder : ISingletonDependency
    {
        private MyGetApiResponse _response;

        public ILogger<MyGetPackageListFinder> Logger { get; set; }

        public MyGetPackageListFinder()
        {
            Logger = NullLogger<MyGetPackageListFinder>.Instance;
        }

        public async Task<MyGetApiResponse> GetPackagesAsync()
        {
            if (_response != null)
            {
                return _response;
            }

            try
            {
                using (var client = new CliHttpClient(TimeSpan.FromMinutes(10)))
                {
                    var responseMessage = await client.GetAsync(
                        $"{CliUrls.WwwRocketIo}api/myget/packages/"
                    );

                    _response = JsonConvert.DeserializeObject<MyGetApiResponse>(Encoding.Default.GetString(await responseMessage.Content.ReadAsByteArrayAsync()));
                }
            }
            catch (Exception)
            {
                Logger.LogError("Unable to get latest preview version.");
                throw;
            }

            return _response;
        }

    }
}
