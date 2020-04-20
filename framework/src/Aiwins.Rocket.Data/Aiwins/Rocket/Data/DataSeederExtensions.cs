using System;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Data {
    public static class DataSeederExtensions {
        public static Task SeedAsync (this IDataSeeder seeder, Guid? tenantId = null) {
            return seeder.SeedAsync (new DataSeedContext (tenantId));
        }
    }
}