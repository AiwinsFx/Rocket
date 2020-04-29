using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.SettingManagement.EntityFrameworkCore {
    [ConnectionStringName (RocketSettingManagementDbProperties.ConnectionStringName)]
    public interface ISettingManagementDbContext : IEfCoreDbContext {
        DbSet<Setting> Settings { get; set; }
    }
}