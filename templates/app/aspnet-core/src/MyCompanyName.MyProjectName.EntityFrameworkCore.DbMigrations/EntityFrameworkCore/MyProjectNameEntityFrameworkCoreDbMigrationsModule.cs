using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    [DependsOn(
        typeof(MyProjectNameEntityFrameworkCoreModule)
        )]
    public class MyProjectNameEntityFrameworkCoreDbMigrationsModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<MyProjectNameMigrationsDbContext>();
        }
    }
}
