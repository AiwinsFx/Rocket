using System;
using Aiwins.Rocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.ClientSimulation.Demo {
    public class Startup {
        public void ConfigureServices (IServiceCollection services) {
            services.AddApplication<ClientSimulationDemoModule> ();
        }

        public void Configure (IApplicationBuilder app) {
            app.InitializeApplication ();
        }
    }
}