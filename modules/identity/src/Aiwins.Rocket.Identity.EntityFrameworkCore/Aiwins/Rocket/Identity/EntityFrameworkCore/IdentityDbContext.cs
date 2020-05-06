using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.Identity.EntityFrameworkCore {
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    [ConnectionStringName (RocketIdentityDbProperties.ConnectionStringName)]
    public class IdentityDbContext : RocketDbContext<IdentityDbContext>, IIdentityDbContext {
        public DbSet<IdentityUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

        public DbSet<IdentityClaimType> ClaimTypes { get; set; }

        public IdentityDbContext (DbContextOptions<IdentityDbContext> options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.ConfigureIdentity ();
        }
    }
}