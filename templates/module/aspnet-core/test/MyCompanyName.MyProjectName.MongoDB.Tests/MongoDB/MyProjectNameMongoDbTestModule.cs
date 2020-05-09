using System;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName.MongoDB
{
    [DependsOn(
        typeof(MyProjectNameTestBaseModule),
        typeof(MyProjectNameMongoDbModule)
        )]
    public class MyProjectNameMongoDbTestModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/') +
                                   "Db_" +
                                    Guid.NewGuid().ToString("N");

            Configure<RocketDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}