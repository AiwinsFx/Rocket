using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Localization.ExceptionHandling;
using Aiwins.Rocket.Modularity;
using Aiwins.Docs.Localization;

namespace Aiwins.Docs
{
    [DependsOn(typeof(RocketLocalizationModule))]
    public class DocsDomainSharedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources.Add<DocsResource>("en");
            });
            
            Configure<RocketExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Aiwins.Docs.Domain", typeof(DocsResource));
            });
        }
    }
}
