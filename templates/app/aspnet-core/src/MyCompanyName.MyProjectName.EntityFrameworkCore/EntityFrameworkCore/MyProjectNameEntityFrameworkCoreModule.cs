using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AuditLogging.EntityFrameworkCore;
using Aiwins.Rocket.BackgroundJobs.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore.SqlServer;
using Aiwins.Rocket.FeatureManagement.EntityFrameworkCore;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.IdentityServer.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Rocket.TenantManagement.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    [DependsOn(
        typeof(MyProjectNameDomainModule),
        typeof(RocketIdentityEntityFrameworkCoreModule),
        typeof(RocketIdentityServerEntityFrameworkCoreModule),
        typeof(RocketPermissionManagementEntityFrameworkCoreModule),
        typeof(RocketSettingManagementEntityFrameworkCoreModule),
        typeof(RocketEntityFrameworkCoreSqlServerModule),
        typeof(RocketBackgroundJobsEntityFrameworkCoreModule),
        typeof(RocketAuditLoggingEntityFrameworkCoreModule),
        typeof(RocketTenantManagementEntityFrameworkCoreModule),
        typeof(RocketFeatureManagementEntityFrameworkCoreModule)
        )]
    public class MyProjectNameEntityFrameworkCoreModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            MyProjectNameEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<MyProjectNameDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<RocketDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also MyProjectNameMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}
