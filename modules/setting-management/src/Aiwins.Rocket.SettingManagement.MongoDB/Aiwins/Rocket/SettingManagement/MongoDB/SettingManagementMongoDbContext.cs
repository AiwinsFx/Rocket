using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.SettingManagement.MongoDB {
    [ConnectionStringName (RocketSettingManagementDbProperties.ConnectionStringName)]
    public class SettingManagementMongoDbContext : RocketMongoDbContext, ISettingManagementMongoDbContext {
        public IMongoCollection<Setting> Settings => Collection<Setting> ();

        protected override void CreateModel (IMongoModelBuilder modelBuilder) {
            base.CreateModel (modelBuilder);

            modelBuilder.ConfigureSettingManagement ();
        }
    }
}