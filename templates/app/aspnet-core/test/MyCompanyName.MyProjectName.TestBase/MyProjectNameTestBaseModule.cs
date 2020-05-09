using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket;
using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.BackgroundJobs;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.IdentityServer;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Threading;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(RocketAutofacModule),
        typeof(RocketTestBaseModule),
        typeof(RocketAuthorizationModule),
        typeof(MyProjectNameDomainModule)
        )]
    public class MyProjectNameTestBaseModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<RocketIdentityServerBuilderOptions>(options =>
            {
                options.AddDeveloperSigningCredential = false;
            });

            PreConfigure<IIdentityServerBuilder>(identityServerBuilder =>
            {
                identityServerBuilder.AddDeveloperSigningCredential(false, System.Guid.NewGuid().ToString());
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });

            context.Services.AddAlwaysAllowAuthorization();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}
