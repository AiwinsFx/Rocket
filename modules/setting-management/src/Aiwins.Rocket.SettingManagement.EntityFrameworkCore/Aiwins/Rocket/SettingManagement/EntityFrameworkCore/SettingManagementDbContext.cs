using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.SettingManagement.EntityFrameworkCore {
    [ConnectionStringName (RocketSettingManagementDbProperties.ConnectionStringName)]
    public class SettingManagementDbContext : RocketDbContext<SettingManagementDbContext>, ISettingManagementDbContext {
        public DbSet<Setting> Settings { get; set; }

        public SettingManagementDbContext (DbContextOptions<SettingManagementDbContext> options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.ConfigureSettingManagement ();
        }
    }
}