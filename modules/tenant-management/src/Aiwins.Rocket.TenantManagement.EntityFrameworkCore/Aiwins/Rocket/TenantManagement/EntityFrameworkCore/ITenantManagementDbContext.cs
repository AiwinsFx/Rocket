using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.TenantManagement.EntityFrameworkCore {
    [ConnectionStringName (RocketTenantManagementDbProperties.ConnectionStringName)]
    public interface ITenantManagementDbContext : IEfCoreDbContext {
        DbSet<Tenant> Tenants { get; set; }

        DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
    }
}