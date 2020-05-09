using Aiwins.Rocket.EntityFrameworkCore.SqlServer;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Docs.EntityFrameworkCore;

namespace AiwinsDocs.EntityFrameworkCore
{
    [DependsOn(
        typeof(DocsEntityFrameworkCoreModule),
        typeof(RocketIdentityEntityFrameworkCoreModule),
        typeof(RocketPermissionManagementEntityFrameworkCoreModule),
        typeof(RocketSettingManagementEntityFrameworkCoreModule),
        typeof(RocketEntityFrameworkCoreSqlServerModule))]
    public class AiwinsDocsEntityFrameworkCoreModule : RocketModule
    {
        
    }
}
