using System;
using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Http.Modeling;
using Aiwins.Rocket.Http.ProxyScripting.Configuration;
using Aiwins.Rocket.Http.ProxyScripting.Generators;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Minify.Scripts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Http.ProxyScripting {
    public class ProxyScriptManager : IProxyScriptManager, ITransientDependency {
        private readonly IApiDescriptionModelProvider _modelProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProxyScriptManagerCache _cache;
        private readonly RocketApiProxyScriptingOptions _options;

        public ProxyScriptManager (
            IApiDescriptionModelProvider modelProvider,
            IServiceProvider serviceProvider,
            IJsonSerializer jsonSerializer,
            IProxyScriptManagerCache cache,
            IOptions<RocketApiProxyScriptingOptions> options) {
            _modelProvider = modelProvider;
            _serviceProvider = serviceProvider;
            _jsonSerializer = jsonSerializer;
            _cache = cache;
            _options = options.Value;
        }

        public string GetScript (ProxyScriptingModel scriptingModel) {
            var cacheKey = CreateCacheKey (scriptingModel);

            if (scriptingModel.UseCache) {
                return _cache.GetOrAdd (cacheKey, () => CreateScript (scriptingModel));
            }

            var script = CreateScript (scriptingModel);
            _cache.Set (cacheKey, script);
            return script;
        }

        private string CreateScript (ProxyScriptingModel scriptingModel) {
            var apiModel = _modelProvider.CreateApiModel (new ApplicationApiDescriptionModelRequestDto { IncludeTypes = false });

            if (scriptingModel.IsPartialRequest ()) {
                apiModel = apiModel.CreateSubModel (scriptingModel.Modules, scriptingModel.Controllers, scriptingModel.Actions);
            }

            var generatorType = _options.Generators.GetOrDefault (scriptingModel.GeneratorType);
            if (generatorType == null) {
                throw new RocketException ($"Could not find a proxy script generator with given name: {scriptingModel.GeneratorType}");
            }

            using (var scope = _serviceProvider.CreateScope ()) {
                return scope.ServiceProvider.GetRequiredService (generatorType).As<IProxyScriptGenerator> ().CreateScript (apiModel);
            }
        }

        private string CreateCacheKey (ProxyScriptingModel model) {
            return _jsonSerializer.Serialize (model).ToMd5 ();
        }
    }
}