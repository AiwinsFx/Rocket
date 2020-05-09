using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Validation;

namespace Aiwins.Rocket.ObjectExtending {
    [DependsOn (
        typeof (RocketLocalizationAbstractionsModule),
        typeof (RocketValidationAbstractionsModule)
    )]
    public class RocketObjectExtendingModule : RocketModule {

    }
}