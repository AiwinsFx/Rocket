using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    public class MyProjectNameHttpApiHostMigrationsDbContext : RocketDbContext<MyProjectNameHttpApiHostMigrationsDbContext>
    {
        public MyProjectNameHttpApiHostMigrationsDbContext(DbContextOptions<MyProjectNameHttpApiHostMigrationsDbContext> options)
            : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureMyProjectName();
        }
    }
}
