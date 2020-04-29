using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.PermissionManagement.EntityFrameworkCore {
    [ConnectionStringName (RocketPermissionManagementDbProperties.ConnectionStringName)]
    public class PermissionManagementDbContext : RocketDbContext<PermissionManagementDbContext>, IPermissionManagementDbContext {
        public DbSet<PermissionGrant> PermissionGrants { get; set; }

        public PermissionManagementDbContext (DbContextOptions<PermissionManagementDbContext> options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.ConfigurePermissionManagement ();
        }
    }
}