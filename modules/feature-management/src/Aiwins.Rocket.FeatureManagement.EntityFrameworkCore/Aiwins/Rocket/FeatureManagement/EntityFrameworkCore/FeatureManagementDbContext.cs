using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;

namespace Aiwins.Rocket.FeatureManagement.EntityFrameworkCore
{
    [ConnectionStringName(FeatureManagementDbProperties.ConnectionStringName)]
    public class FeatureManagementDbContext : RocketDbContext<FeatureManagementDbContext>, IFeatureManagementDbContext
    {
        public DbSet<FeatureValue> FeatureValues { get; set; }

        public FeatureManagementDbContext(DbContextOptions<FeatureManagementDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureFeatureManagement();
        }
    }
}