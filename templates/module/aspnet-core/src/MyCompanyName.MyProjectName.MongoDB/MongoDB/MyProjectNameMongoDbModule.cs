using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB
{
    [DependsOn(
        typeof(MyProjectNameDomainModule),
        typeof(RocketMongoDbModule)
        )]
    public class MyProjectNameMongoDbModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<MyProjectNameMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
