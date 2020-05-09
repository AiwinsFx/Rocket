using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AuditLogging.MongoDB;
using Aiwins.Rocket.BackgroundJobs.MongoDB;
using Aiwins.Rocket.FeatureManagement.MongoDB;
using Aiwins.Rocket.Identity.MongoDB;
using Aiwins.Rocket.IdentityServer.MongoDB;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.MongoDB;
using Aiwins.Rocket.SettingManagement.MongoDB;
using Aiwins.Rocket.TenantManagement.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB
{
    [DependsOn(
        typeof(MyProjectNameDomainModule),
        typeof(RocketPermissionManagementMongoDbModule),
        typeof(RocketSettingManagementMongoDbModule),
        typeof(RocketIdentityMongoDbModule),
        typeof(RocketIdentityServerMongoDbModule),
        typeof(RocketBackgroundJobsMongoDbModule),
        typeof(RocketAuditLoggingMongoDbModule),
        typeof(RocketTenantManagementMongoDbModule),
        typeof(RocketFeatureManagementMongoDbModule)
        )]
    public class MyProjectNameMongoDbModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<MyProjectNameMongoDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });
        }
    }
}
