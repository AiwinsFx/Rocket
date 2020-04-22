using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.ApiVersioning {
    public class RocketApiVersioningAbstractionsModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddSingleton<IRequestedApiVersion> (NullRequestedApiVersion.Instance);
        }
    }
}