using MyCompanyName.MyProjectName.EntityFrameworkCore;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.BackgroundJobs;
using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName.DbMigrator
{
    [DependsOn(
        typeof(RocketAutofacModule),
        typeof(MyProjectNameEntityFrameworkCoreDbMigrationsModule),
        typeof(MyProjectNameApplicationContractsModule)
        )]
    public class MyProjectNameDbMigratorModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
