using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;
using AiwinsDocs.EntityFrameworkCore;

namespace AiwinsDocs.Migrator
{
    [DependsOn(typeof(AiwinsDocsEntityFrameworkCoreModule))]
    public class AiwinsDocsMigratorModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddRocketDbContext<AiwinsDocsDbContext>();

            Configure<RocketDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = configuration["ConnectionString"];
            });

            Configure<RocketDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
        }
    }
}