using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.FeatureManagement.EntityFrameworkCore {
    [ConnectionStringName (FeatureManagementDbProperties.ConnectionStringName)]
    public interface IFeatureManagementDbContext : IEfCoreDbContext {
        DbSet<FeatureValue> FeatureValues { get; set; }
    }
}