using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.AuditLogging.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Rocket.TenantManagement.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    public class UnifiedDbContext : RocketDbContext<UnifiedDbContext>
    {
        public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigureAuditLogging();
            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureTenantManagement();
            modelBuilder.ConfigureMyProjectName();
        }
    }
}
