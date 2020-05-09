using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Cli.Http;
using Aiwins.Rocket.Cli.ProjectBuilding.Building;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.Cli.ProjectBuilding
{
    public class ModuleInfoProvider : IModuleInfoProvider, ITransientDependency
    {
        public IJsonSerializer JsonSerializer { get; }
        public ICancellationTokenProvider CancellationTokenProvider { get; }
        public IRemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }

        public ModuleInfoProvider(
            IJsonSerializer jsonSerializer,
            ICancellationTokenProvider cancellationTokenProvider,
            IRemoteServiceExceptionHandler remoteServiceExceptionHandler)
        {
            JsonSerializer = jsonSerializer;
            CancellationTokenProvider = cancellationTokenProvider;
            RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
        }

        public async Task<ModuleInfo> GetAsync(string name)
        {
            var moduleList = await GetModuleListInternalAsync();

            var module = moduleList.FirstOrDefault(m => m.Name == name);

            if (module == null)
            {
                throw new Exception("Module not found!");
            }

            return module;
        }

        public async Task<List<ModuleInfo>> GetModuleListAsync()
        {
            return await GetModuleListInternalAsync();
        }

        private async Task<List<ModuleInfo>> GetModuleListInternalAsync()
        {
            using (var client = new CliHttpClient())
            {
                var responseMessage = await client.GetAsync(
                    $"{CliUrls.WwwRocketIo}api/download/modules/",
                    CancellationTokenProvider.Token
                );

                await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync(responseMessage);
                var result = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ModuleInfo>>(result);
            }
        }
    }
}
