using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.AuditLogging.EntityFrameworkCore;
using Aiwins.Rocket.BackgroundJobs.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.FeatureManagement.EntityFrameworkCore;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.IdentityServer.EntityFrameworkCore;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Rocket.TenantManagement.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See MyProjectNameDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class MyProjectNameMigrationsDbContext : RocketDbContext<MyProjectNameMigrationsDbContext>
    {
        public MyProjectNameMigrationsDbContext(DbContextOptions<MyProjectNameMigrationsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside the ConfigureMyProjectName method */

            builder.ConfigureMyProjectName();
        }
    }
}