using Aiwins.Rocket.Modularity;

namespace Aiwins.Docs.Admin
{
    [DependsOn(
        typeof(DocsAdminApplicationContractsModule))]
    public class DocsAdminHttpApiClientModule : RocketModule
    {
        //TODO: Create client proxies!
    }
}
