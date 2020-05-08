using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.AuditLogging.EntityFrameworkCore {
    [ConnectionStringName (RocketAuditLoggingDbProperties.ConnectionStringName)]
    public interface IAuditLoggingDbContext : IEfCoreDbContext {
        DbSet<AuditLog> AuditLogs { get; set; }
    }
}