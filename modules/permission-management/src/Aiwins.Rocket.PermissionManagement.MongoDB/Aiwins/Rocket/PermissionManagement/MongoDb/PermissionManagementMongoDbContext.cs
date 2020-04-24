using MongoDB.Driver;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.PermissionManagement.MongoDB
{
    [ConnectionStringName(RocketPermissionManagementDbProperties.ConnectionStringName)]
    public class PermissionManagementMongoDbContext : RocketMongoDbContext, IPermissionManagementMongoDbContext
    {
        public IMongoCollection<PermissionGrant> PermissionGrants => Collection<PermissionGrant>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigurePermissionManagement();
        }
    }
}