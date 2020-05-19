using Aiwins.ClientSimulation.Demo.Scenarios;
using Aiwins.ClientSimulation.Scenarios;
using Aiwins.Rocket;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Aiwins.ClientSimulation.Demo {
    [DependsOn (
        typeof (RocketAspNetCoreMvcUiBasicThemeModule),
        typeof (RocketAutofacModule),
        typeof (ClientSimulationWebModule)
    )]
    public class ClientSimulationDemoModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<ClientSimulationOptions> (options => {
                options.Scenarios.Add (
                    new ScenarioConfiguration (
                        typeof (DemoScenario),
                        clientCount : 20
                    )
                );
            });
        }

        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            var app = context.GetApplicationBuilder ();
            var env = context.GetEnvironment ();

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseVirtualFiles ();
            app.UseRouting ();
            app.UseConfiguredEndpoints ();
        }
    }
}