using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.Identity.EntityFrameworkCore {
    [ConnectionStringName (RocketIdentityDbProperties.ConnectionStringName)]
    public interface IIdentityDbContext : IEfCoreDbContext {
        DbSet<IdentityUser> Users { get; set; }

        DbSet<IdentityRole> Roles { get; set; }

        DbSet<IdentityClaimType> ClaimTypes { get; set; }
    }
}