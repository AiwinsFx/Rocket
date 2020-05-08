using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.BackgroundJobs.EntityFrameworkCore {
    [ConnectionStringName (BackgroundJobsDbProperties.ConnectionStringName)]
    public interface IBackgroundJobsDbContext : IEfCoreDbContext {
        DbSet<BackgroundJobRecord> BackgroundJobs { get; }
    }
}