using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    [DependsOn(
        typeof(MyProjectNameTestBaseModule),
        typeof(MyProjectNameEntityFrameworkCoreModule)
        )]
    public class MyProjectNameEntityFrameworkCoreTestModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var sqliteConnection = CreateDatabaseAndGetConnection();

            Configure<RocketDbContextOptions>(options =>
            {
                options.Configure(rocketDbContextConfigurationContext =>
                {
                    rocketDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
                });
            });
        }
        
        private static SqliteConnection CreateDatabaseAndGetConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            new MyProjectNameDbContext(
                new DbContextOptionsBuilder<MyProjectNameDbContext>().UseSqlite(connection).Options
            ).GetService<IRelationalDatabaseCreator>().CreateTables();
            
            return connection;
        }
    }
}
