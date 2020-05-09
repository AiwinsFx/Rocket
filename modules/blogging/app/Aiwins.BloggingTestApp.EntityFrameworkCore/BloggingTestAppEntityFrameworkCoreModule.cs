using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.EntityFrameworkCore.SqlServer;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Blogging.EntityFrameworkCore;

namespace Aiwins.BloggingTestApp.EntityFrameworkCore
{
    [DependsOn(
        typeof(BloggingEntityFrameworkCoreModule),
        typeof(RocketIdentityEntityFrameworkCoreModule),
        typeof(RocketPermissionManagementEntityFrameworkCoreModule),
        typeof(RocketSettingManagementEntityFrameworkCoreModule),
        typeof(RocketEntityFrameworkCoreSqlServerModule))]
    public class BloggingTestAppEntityFrameworkCoreModule : RocketModule
    {

    }
}
