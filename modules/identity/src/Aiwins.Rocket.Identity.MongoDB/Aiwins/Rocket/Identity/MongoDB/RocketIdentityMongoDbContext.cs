using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.Identity.MongoDB {
    [ConnectionStringName (RocketIdentityDbProperties.ConnectionStringName)]
    public class RocketIdentityMongoDbContext : RocketMongoDbContext, IRocketIdentityMongoDbContext {
        public IMongoCollection<IdentityUser> Users => Collection<IdentityUser> ();

        public IMongoCollection<IdentityRole> Roles => Collection<IdentityRole> ();

        public IMongoCollection<IdentityClaimType> ClaimTypes => Collection<IdentityClaimType> ();

        protected override void CreateModel (IMongoModelBuilder modelBuilder) {
            base.CreateModel (modelBuilder);

            modelBuilder.ConfigureIdentity ();
        }
    }
}