using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    [DependsOn(
        typeof(MyProjectNameDomainModule),
        typeof(RocketEntityFrameworkCoreModule)
    )]
    public class MyProjectNameEntityFrameworkCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<MyProjectNameDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}