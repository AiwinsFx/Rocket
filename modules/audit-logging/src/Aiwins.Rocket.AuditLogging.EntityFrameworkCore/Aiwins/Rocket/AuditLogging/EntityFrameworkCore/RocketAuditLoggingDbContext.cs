using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.AuditLogging.EntityFrameworkCore {
    [ConnectionStringName (RocketAuditLoggingDbProperties.ConnectionStringName)]
    public class RocketAuditLoggingDbContext : RocketDbContext<RocketAuditLoggingDbContext>, IAuditLoggingDbContext {
        public DbSet<AuditLog> AuditLogs { get; set; }

        public RocketAuditLoggingDbContext (DbContextOptions<RocketAuditLoggingDbContext> options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.ConfigureAuditLogging ();
        }
    }
}