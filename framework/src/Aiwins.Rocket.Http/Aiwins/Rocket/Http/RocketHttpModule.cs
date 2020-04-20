using Aiwins.Rocket.Http.ProxyScripting.Configuration;
using Aiwins.Rocket.Http.ProxyScripting.Generators.JQuery;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Minify;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Http {
    [DependsOn (typeof (RocketHttpAbstractionsModule))]
    [DependsOn (typeof (RocketJsonModule))]
    [DependsOn (typeof (RocketMinifyModule))]
    public class RocketHttpModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketApiProxyScriptingOptions> (options => {
                options.Generators[JQueryProxyScriptGenerator.Name] = typeof (JQueryProxyScriptGenerator);
            });
        }
    }
}