using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Identity.EntityFrameworkCore;
using Aiwins.Rocket.PermissionManagement.EntityFrameworkCore;
using Aiwins.Rocket.SettingManagement.EntityFrameworkCore;
using Aiwins.Blogging.EntityFrameworkCore;

namespace Aiwins.BloggingTestApp.EntityFrameworkCore
{
    public class BloggingTestAppDbContext : RocketDbContext<BloggingTestAppDbContext>
    {
        public BloggingTestAppDbContext(DbContextOptions<BloggingTestAppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureBlogging();
        }
    }
}
