using MongoDB.Driver;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.AuditLogging.MongoDB
{
    [ConnectionStringName(RocketAuditLoggingDbProperties.ConnectionStringName)]
    public interface IAuditLoggingMongoDbContext : IRocketMongoDbContext
    {
        IMongoCollection<AuditLog> AuditLogs { get; }
    }
}
