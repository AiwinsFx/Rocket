using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.AuditLogging.MongoDB {
    [ConnectionStringName (RocketAuditLoggingDbProperties.ConnectionStringName)]
    public interface IAuditLoggingMongoDbContext : IRocketMongoDbContext {
        IMongoCollection<AuditLog> AuditLogs { get; }
    }
}