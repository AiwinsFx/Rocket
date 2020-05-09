using System.Threading.Tasks;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.DependencyInjection;

namespace MyCompanyName.MyProjectName
{
    public class MyProjectNameTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */

            return Task.CompletedTask;
        }
    }
}