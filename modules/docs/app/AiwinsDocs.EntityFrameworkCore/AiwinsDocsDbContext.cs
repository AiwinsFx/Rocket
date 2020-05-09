using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Docs.EntityFrameworkCore;

namespace AiwinsDocs.EntityFrameworkCore
{
    public class AiwinsDocsDbContext : RocketDbContext<AiwinsDocsDbContext>
    {
        public AiwinsDocsDbContext(DbContextOptions<AiwinsDocsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureDocs();
        }
    }
}
