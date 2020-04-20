using System.Threading.Tasks;

namespace Aiwins.Rocket.Data {
    public interface IDataSeedContributor {
        Task SeedAsync (DataSeedContext context);
    }
}