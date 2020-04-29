using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.PermissionManagement.EntityFrameworkCore {
    [ConnectionStringName (RocketPermissionManagementDbProperties.ConnectionStringName)]
    public interface IPermissionManagementDbContext : IEfCoreDbContext {
        DbSet<PermissionGrant> PermissionGrants { get; set; }
    }
}