using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.BackgroundJobs.EntityFrameworkCore {
    [ConnectionStringName (BackgroundJobsDbProperties.ConnectionStringName)]
    public class BackgroundJobsDbContext : RocketDbContext<BackgroundJobsDbContext>, IBackgroundJobsDbContext {
        public DbSet<BackgroundJobRecord> BackgroundJobs { get; set; }

        public BackgroundJobsDbContext (DbContextOptions<BackgroundJobsDbContext> options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.ConfigureBackgroundJobs ();
        }
    }
}