using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.ClientSimulation.Demo {
    [Dependency (ReplaceServices = true)]
    public class BrandingProvider : DefaultBrandingProvider {
        public override string AppName => "Client Simulation";
    }
}