using Microsoft.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Users;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Users.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See MyProjectNameMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class MyProjectNameDbContext : RocketDbContext<MyProjectNameDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside MyProjectNameDbContextModelCreatingExtensions.ConfigureMyProjectName
         */

        public MyProjectNameDbContext(DbContextOptions<MyProjectNameDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(RocketIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "RocketUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureRocketUser();

                /* Configure mappings for your additional properties
                 * Also see the MyProjectNameEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureMyProjectName method */

            builder.ConfigureMyProjectName();
        }
    }
}
