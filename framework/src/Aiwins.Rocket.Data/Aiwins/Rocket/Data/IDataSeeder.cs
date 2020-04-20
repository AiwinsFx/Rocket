using System.Threading.Tasks;

namespace Aiwins.Rocket.Data {
    public interface IDataSeeder {
        Task SeedAsync (DataSeedContext context);
    }
}