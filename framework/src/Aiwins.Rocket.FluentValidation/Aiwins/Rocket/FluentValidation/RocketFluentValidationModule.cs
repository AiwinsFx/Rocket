using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Validation;

namespace Aiwins.Rocket.FluentValidation {
    [DependsOn (
        typeof (RocketValidationModule)
    )]
    public class RocketFluentValidationModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddConventionalRegistrar (new RocketFluentValidationConventionalRegistrar ());
        }
    }
}