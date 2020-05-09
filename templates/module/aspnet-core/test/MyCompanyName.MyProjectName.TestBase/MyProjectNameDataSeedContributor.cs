using System.Threading.Tasks;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Guids;

namespace MyCompanyName.MyProjectName
{
    public class MyProjectNameDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;

        public MyProjectNameDataSeedContributor(
            IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }
        
        public Task SeedAsync(DataSeedContext context)
        {
            /* Instead of returning the Task.CompletedTask, you can insert your test data
             * at this point!
             */

            return Task.CompletedTask;
        }
    }
}