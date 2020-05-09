using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    [ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
    public class MyProjectNameDbContext : RocketDbContext<MyProjectNameDbContext>, IMyProjectNameDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public MyProjectNameDbContext(DbContextOptions<MyProjectNameDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureMyProjectName();
        }
    }
}