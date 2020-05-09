using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Application;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainModule),
        typeof(MyProjectNameApplicationContractsModule),
        typeof(RocketDddApplicationModule),
        typeof(RocketAutoMapperModule)
        )]
    public class MyProjectNameApplicationModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MyProjectNameApplicationModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddMaps<MyProjectNameApplicationModule>(validate: true);
            });
        }
    }
}
