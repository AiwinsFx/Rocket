using Aiwins.Rocket.Application;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Docs
{
    [DependsOn(
        typeof(DocsDomainSharedModule),
        typeof(RocketDddApplicationModule)
        )]
    public class DocsApplicationContractsModule : RocketModule
    {
        
    }
}
