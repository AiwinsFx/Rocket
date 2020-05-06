using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.Identity.MongoDB {
    [ConnectionStringName (RocketIdentityDbProperties.ConnectionStringName)]
    public interface IRocketIdentityMongoDbContext : IRocketMongoDbContext {
        IMongoCollection<IdentityUser> Users { get; }

        IMongoCollection<IdentityRole> Roles { get; }

        IMongoCollection<IdentityClaimType> ClaimTypes { get; }
    }
}