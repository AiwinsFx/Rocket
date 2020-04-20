using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Uow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Data {
    //TODO: 考虑是否迁移至 Aiwins.Rocket.Data.Seeding 命名空间?
    public class DataSeeder : IDataSeeder, ITransientDependency {
        protected IHybridServiceScopeFactory ServiceScopeFactory { get; }
        protected RocketDataSeedOptions Options { get; }

        public DataSeeder (
            IOptions<RocketDataSeedOptions> options,
            IHybridServiceScopeFactory serviceScopeFactory) {
            ServiceScopeFactory = serviceScopeFactory;
            Options = options.Value;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync (DataSeedContext context) {
            using (var scope = ServiceScopeFactory.CreateScope ()) {
                foreach (var contributorType in Options.Contributors) {
                    var contributor = (IDataSeedContributor) scope
                        .ServiceProvider
                        .GetRequiredService (contributorType);

                    await contributor.SeedAsync (context);
                }
            }
        }
    }
}